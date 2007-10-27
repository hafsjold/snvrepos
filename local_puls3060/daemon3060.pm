#!/usr/bin/perl -w
#----------------------------------------------------------------------------
#   daemon3060.pm - simple daemon object with logging functions
#
#   Copyright (c) 2001-2002 Baltasar Cevc, Walter Werther
#
#   This program is free software; you can redistribute it and/or modify
#   it under the terms of the GNU General Public License as published by
#   the Free Software Foundation; either version 2 of the License, or
#   (at your option) any later version.
#
#   DISCLAIMER: THIS SOFTWARE AND DOCUMENTATION IS PROVIDED "AS IS," AND
#   COPYRIGHT HOLDERS MAKE NO REPRESENTATIONS OR WARRANTIES, EXPRESS OR
#   IMPLIED, INCLUDING BUT NOT LIMITED TO, WARRANTIES OF MERCHANTABILITY
#   OR FITNESS FOR ANY PARTICULAR PURPOSE OR THAT THE USE OF THE SOFTWARE
#   OR DOCUMENTATION WILL NOT INFRINGE ANY THIRD PARTY PATENTS, COPYRIGHTS,
#   TRADEMARKS OR OTHER RIGHTS.
#   IF YOU USE THIS SOFTWARE, YOU DO SO AT YOUR OWN RISK.
#
#   See this internet site for more details: http://technik.juz-kirchheim.de/
#
#   Creation:       04.11.01    bc+ww
#   Last Update:    30.08.02    bc
#   Version:        1.0.1
# ----------------------------------------------------------------------------
##################################################
# Package name and version
##################################################
package daemon3060;

use vars qw($VERSION @ISA @EXPORT @EXPORT_OK);
$VERSION = "1.0.1";

##################################################
# Dependencies
##################################################
# need at least perl 5.8
use 5.008;
# warn and error reporting functions
use Carp;
# POSIX setsid();
use POSIX qw(setsid);
# Logging features
use Sys::Syslog;

##################################################
# Pragma
##################################################
use strict;
require Exporter;
@ISA     = qw(Exporter AutoLoader);
@EXPORT  = qw( );

##################################################
# Singleton
##################################################
use vars qw($daemon);
my $WNOHANG = get_system_nohang();

##################################################
# some functions
##################################################
# Handler to quit the daemon (not object-oriented)
sub inthandler_quit {
    my $sig = shift;
    $daemon->dolog ("Exitting on Signal SIG$sig.\n");
    $daemon->stop();
}

##################################################
# logging functions
##################################################
# set or get log level
sub loglevel {
   my $self = shift;
   my $level = shift;

   if (defined $level) {
      $self->{'loglevel'} = $level;
   } else {
      return $self->{'loglevel'};
   };
}

# open log destination
sub logto {
   my $self = shift;
   croak ("daemon3060::logto(): syntax: logto ([<filename>||//syslog||//debug]);") 
      unless (@_ <= 1);

   if (@_ == 1) {
      SWITCH: {
         ($_[0] eq '//syslog') && do { 
            Sys::Syslog::setlogsock('unix');
            Sys::Syslog::openlog ($self -> {'name'}, 'cons,pid', 'local');
            $self->{'logto'} = "//syslog";
            $self->{'logto_syslog'} = 1;
            last SWITCH;
         };
         ($_[0] eq '//debug') && do {
            $self->{'logto'} = "//debug";
            last SWITCH;
         };
         open (DAEMON__LOGFILE, ">>$_[0]") || 
            croak ("cannot open log file '$_[0]': $!");
         $self->{'logto'} = "//file";
         $self->{'logto_file'} = "$_[0]";
	 last SWITCH;
      };
   } else {
      return $self->{'logto'};
   };
  
};

# close log destination
sub logclose {
   my $self = shift;

   if ($self->{'logto_syslog'}) {
      Sys::Syslog::closelog;
   };
   if ($self->{'logto_file'}) {
      close (DAEMON__LOGFILE) || carp "could not close log file '$self->{'logto_file'}'";
   };
};

# do logging
sub dolog {
   my $self = shift;
   my $string = shift;
   my $level = shift;

   if (defined $level && defined $self->{'loglevel'}) {
      return unless ($level <= $self->{'loglevel'});
   };

   if ((!$string) || ($string eq '')) {
      $string = "-- MARK --";
   };
   SWITCH: {
      ($self->{'logto'} eq '//syslog') && do {
         Sys::Syslog::syslog('notice', $string);
         last SWITCH;
      };
      ($self->{'logto'} eq '//debug') && do {
         print STDERR $string."\n";
         last SWITCH;
      };
      ($self->{'logto'} eq '//file') && do {
         print DAEMON__LOGFILE $string."\n";
         last SWITCH;
      };
   };
};

##################################################
# object initialisation and creation
##################################################
sub new {
   my $type = shift;
   my $self = {};
   my $blessed;

   $self -> {'pid'} = 0;
   $self -> {'name'} = shift;
   unless ($self -> {'name'} && ($self-> {'name'} ne '')) {
      $self->{'name'} = $0;
      $self->{'name'} =~ s/^.*\///;
      $self->{'name'} =~ s/\.[a-zA-Z0-9]+$//;
   };
   # standard pid-file
   $self -> {'pidfile'} = "/var/run/".$self->{'name'}.".pid";

   $self -> {'parent'} = 0;
   $self -> {'child_properties'} = {};

   $blessed = bless $self;
   $daemon = $blessed;
   
   $blessed -> logto ('//debug');

   $SIG{'QUIT'} = "daemon3060::inthandler_quit";
   
   return $blessed;
};

# set or get pid file 
sub pidfile {
   my $self = shift;
   my $pidf = shift;
   
   if ($pidf) {
      $self ->{'pidfile'} = $pidf;
   } else {
      return $self->{'pidfile'};
   };
}

##################################################
# start daemon
##################################################
# start daemon in detached mode
sub detach {
    my $self = shift; 
    my $childpid;

    if (($self->logto) eq '//debug') {
        $self->logto('//syslog');
    };
    $|=1;
    $childpid=fork();
    if (defined ($childpid))
       {
         if ($childpid==0)
            {  # Child Process
               $SIG{'QUIT'} = "daemon3060::inthandler_quit";
               setsid();
               open (STDIN,  '</dev/null') || $self -> dolog ("Could not detach STDIN - /dev/null is not readable: $!");
               open STDOUT, '>/dev/null' || $self -> dolog ("Could not detach STDOUT - /dev/null is not writeable: $!");
               open STDERR, '>&STDOUT' || $self -> dolog ("Could not detach STDERR - /dev/null is not writeable: $!"); 
               $self->{'runmode'} = 'detached';
               $|=0;
               return $childpid;
            } else
            {  # Parent Process has got child pid
               $self -> {'parent'} = 1;
               open (DAEMON__PIDFILE,"> ".$self->pidfile) ||
                  $self -> dolog ("Could not open PID file: $!");
               print (DAEMON__PIDFILE "$childpid") ||
                  $self -> dolog ("Could not save PID to file: $!");
               close (DAEMON__PIDFILE) || 
                  $self -> dolog ("Could not close/save PID file: $!");
               # parent has done his job and can exit now
               exit 0;
            };
       }
       else
       {
         $self -> dolog ('could not fork. exitting');

         croak "Could not fork()";
         return undef;
       }
    return undef;  
};

# start daemon in debugging mode
sub debug {
    my $self = shift;

    $self->{'runmode'} = 'debug';
    open DAEMON__PIDFILE,">".($self->pidfile) || $self -> log ("Could not open PID file: $!");
    print DAEMON__PIDFILE "$$" || $self -> (log "Could not save PID to file: $!");
    close DAEMON__PIDFILE || $self -> log ("Could not save PID: $!");

    # make SigINT (Ctrl-C) terminate the program properly
    $SIG{'INT'} = "daemon3060::inthandler_quit";

    return $$;
};

##################################################
# fork a child	process
##################################################
sub forkchild {
    my $self = shift; 
    my ($func, @params) = @_;
    my $childpid;

    if (($self->logto) eq '//debug') {
        $self->logto('//syslog');
    };
    $|=1;
    $childpid=fork();
    if (defined ($childpid))
       {
         if ($childpid==0)
            {  # Child Process
               $SIG{'QUIT'} = "daemon3060::inthandler_quit";
               open (STDIN,  '</dev/null') || $self -> dolog ("Could not detach STDIN - /dev/null is not readable: $!");
               open STDOUT, '>/dev/null' || $self -> dolog ("Could not detach STDOUT - /dev/null is not writeable: $!");
               open STDERR, '>&STDOUT' || $self -> dolog ("Could not detach STDERR - /dev/null is not writeable: $!"); 
               $self->{'runmode'} = 'child';
               $|=0;
               
               if(ref($func) eq "CODE") {
	               $func->(@params); 		  # Start perl subroutine
	               exit 0;  
               } 
               else {
                   exec $func, @params;       # Start shell process
                   exit 0;                    # In case something goes wrong
               }
            } else
            {  # Parent Process has got child pid
               $|=0;
               $SIG{'CHLD'} = "daemon3060::inthandler_chld";
               # Register PID
               $self -> {'child_properties'} -> {$childpid}->{'EXIT_STATUS'} = undef;
               if(ref($func) eq "CODE") {
                 $self -> {'child_properties'} -> {$childpid}->{'func'} = $func;
                 $self -> {'child_properties'} -> {$childpid}->{'params'} = @params;
               } 
               return 1;
            };
       }
       else
       {
         $self -> dolog ('could not fork. exitting');

         croak "Could not fork()";
         return undef;
       }
    return undef;  
};

##################################################
# is daemon running?
##################################################
# Check if daemon is running
# and return pid if it does, otherwise 0 will indicate the non-existance
sub check_running
  {
    my $self = shift;

    if (-e $self->{'pidfile'})
    {
       if (open (PIDFH, "<$self->{'pidfile'}"))
       {
         $_ = <PIDFH>;
         close (PIDFH) || carp "Could not close PID file '$self->{'pidfile'}'";
       } else {
         carp "Could not open PID file: $self->{'pidfile'}";
         return undef;
       };
       return $_;
    } else {
      return 0;
    };
};

##################################################
# stop daemon
##################################################
# Quit Daemon

sub stop {
    my $self = shift;
    my $key;
    
    if ( ($self->{'runmode'}) && ($self->{'runmode'} eq 'child') ) {
      ##$self->dolog ("Do not removed the PID file in sub stop\n");
    } else {
      ##$self->dolog ("Try to removed the PID file in sub stop\n");
      unlink "$self->{'pidfile'}" || Sys::Syslog::syslog ('warn',"PID file coul not be removed -> Please do it by hand (Reason: $!).");
    }
    $self -> logclose;
    # Reset interrupt handlers
    foreach $key (keys %SIG)
      {
        $SIG{"$key"}='DEFAULT';
      }
    exit 0;
};

##################################################
# make the daemon quit
##################################################
# Kill the running daemon
# boolean return value indicates the success
sub kill {
   my $self = shift;
   my $pid;
    
   if (-e $self-> {'pidfile'})
   {
      if (open (DAEMON__PIDFH, $self->{'pidfile'}))
      {
        $pid = <DAEMON__PIDFH>;
        close (DAEMON__PIDFH) || carp "Could not close PID file '$self->{'pidfile'}'";
      } else {
        carp "Could not open PID file: $self->{'pidfile'}";
        return 0;
      };
      kill 3, -$pid;
      return 1;
   } else {
     return 0;
   };
};

######################################################################
# Reaps processes, uses the magic WNOHANG constant
######################################################################
sub inthandler_chld {
    my $self = shift;
    my $child;

    if(defined $WNOHANG) {
        foreach my $pid (keys %{$daemon -> {'child_properties'}}) {
            next if defined $daemon->{'child_properties'}->{$pid}->{'EXIT_STATUS'};
            if(my $res = waitpid($pid, $WNOHANG) > 0) {
                # We reaped a truly running process
                $daemon->{'child_properties'}->{$pid}->{'EXIT_STATUS'} = $?;
            } else {
		
            }
        }
    } else { 
        $child = wait();
        $daemon->{'child_properties'}->{$child}->{'EXIT_STATUS'} = $?;
    }
};

#################################################
# prevent unclean exit
##################################################
# clean up when program quits
sub DESTROY {
   my $self = shift;
   # print "BEGIN DESTRUCTOR\n";
   $self -> stop() if ($self->{'runmode'});
   # print "END DESTRUCTOR\n";
};

##################################################
# timer
##################################################
sub wait_seconds {
    my $self = shift;
    select(undef,undef,undef, shift);
};

sub get_system_nohang {
    my $nohang;

    open(SAVEERR, ">&STDERR");
    open(STDERR, ">/dev/null") || return undef;
    close(STDERR);
    eval 'use POSIX ":sys_wait_h"; $nohang = &WNOHANG;';
    open(STDERR, ">&SAVEERR");
    close(SAVEERR);
    return undef if $@;

    return $nohang;
};

1; 

=pod

=head1 NAME

C<daemon3060.pm> - simple daemon controlling object 

=head1 SYNOPSIS

C<my $daemon = new daemon3060;>

C<$daemon->datach;>

C<open LOGFILE, "E<gt>E<GT>daemon.log";>

C<while (sleep 5) {>
    C<print LOGFILE "Hallo, ich arbeite\n";>
C<};>

=head1 COPYRIGHT

daemon3060.pm is Copyright (C) 2001-2002 Baltasar Cevc and Walter Werther.

=head1 DESCRIPTION

Using this object, you can create a simple daemon. The module provides 
a straight-forward interface to start, detach and stop the daemon.

=head1 Method overview

=over 4

=item C<new([daemon-name]> Create a new daemon-controller object

Creates a new object to controll a daemon, using the given name.
If no name is given, the name of the program that calls this function
is used.

=item C<inthandler_quit(E<lt>signameE<gt>)> Handler for the quit signal (will exit daemon)

This is a handler for a quit signal, which will call all methods needed for a clean
exit, afterwards, it will quit the daemon.

=item C<loglevel([level])> Set or get the maximum log level

Get/Set the log level (higher level means more output). You can define the maximum at
will.

=item C<logto([destination])> Set/get the current log destination

If you specify an argument, the log destination will be changed, otherwise it will
return the current destination. Possible destinations are "//syslog" (log to syslog),
"//debug" (log to STDERR), "//file" (log goes to a file) - to set the log to a file,
you just have to call C<logto (E<lt>filenameE<gt>);>.
This function will also open the log destination.

=item C<dolog(E<lt>textE<gt>[, E<lt>levelE<gt>])> Do logging

This function will log the specified text; if a level is given, the text will only be
logged if the level is smaller or equal to the maximum log level.

=item C<logclose()> Close log destination(s)

Closes all opened log destinations. This method is called automatically by the stop
or DESTROY methods. You should not use this unless you really know what you are 
doing.

=item C<pidfile([pidfilename])> Get / Set PID file name

This function can be used to set the PID file path. If it is called without arguments,
it will return the name of the PID file. This method MUST NOT BE USED after having used
C<detach> or C<debug>.

=item C<detach>

Detach the daemon from the tty. If the logging destination is "//debug", "//syslog" will
be used instead.

=item C<wait_seconds(int value)>

Sleeps for a specified number of seconds (give a fractional value if you
want to sleep less than a second).
This function uses the select call.

=back

=head1 BUGS

kill function and check_running will not work for programs that use the 
C<detach> or C<debug> functions more than once, because they relay on the 
PID file which will be overwritten be further detaches or debugs.

=head1 CHANGES

=over 4

=item * Changes until version 1.0.1

This is more or less the same version as 0.1 (old version format), 
except some smaller changes and the introduction of one new method.

In this version, the version number is properly defined and in debug
mode SIGINT (which is issued by pressing Ctrl-C) will end the program 
properly. In this case it does exactly the same as sendingn a SIGTERM.

The C<wait_second(int value)> method was introduced.

=back

=cut