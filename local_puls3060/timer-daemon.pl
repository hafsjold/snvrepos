#!/usr/bin/perl -w
#----------------------------------------------------------------------------
#   timer-daemon.pl - daemon object demonstration file for daemon.pm (v0.1)
#                     this script a timer-controlled daemon which will
#                     print "mark" into the logfile every 50 deci-seconds
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
#   Creation:       04.08.01    bc
#   Last Update:     1.09.01    bc
#   Version:         1. 0. 1 
# ----------------------------------------------------------------------------
# needs daemon.pm (version at least 1.0.1)
use daemon "1.0.1";

##### make your settings here:
# sleep this amount of seconds before beginning a cycle (can be fractions)
my $sleeptime = 0.1;
# print a mark entry into the logfile every x cycles
my $markcycle = 50;

# start daemon 
my $daemon = new daemon; 
# or if we want to change the daemon name
# my $daemon = new daemon "example-daemon";

# the following lines are the defaults, so we don't need them
# $daemon -> pidfile ("/var/run/example-daemon.pid");
# $daemon -> logto ('//syslog);

# counter variable for loop
my $counter = 0;

if ($daemon -> check_running()) {
   $daemon -> kill; # kill daemon if running
} else {
   # otherwise start it
   unless ($ARGV[0]) {
      $daemon -> detach;
   } else {
      $daemon -> debug;
   }
   $daemon -> dolog("started timer-example");
   
   # here, we could do some network io, if we wanted to
   LOOP: {
      $daemon->wait_seconds ($sleeptime);
      if (($counter ++) >= $markcycle) {
         # log "mark" every 5 seconds
         dolog $daemon "mark";
         $counter = 0;
      };
      redo LOOP;
   };
   dolog $daemon "timer-example done its work";
};

# end daemon (should not be reached)
exit 0;

