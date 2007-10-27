#!/usr/bin/perl -w
#----------------------------------------------------------------------------
#   server-daemon.pl - daemon object demonstration file for daemon.pm (v0.1)
#                      a little sample server (displaying the time to
#                      anybody who connects to it)
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
#   Creation:       30.08.01    bc
#   Last Update:    none
#   Version:         1. 0. 1 
# ----------------------------------------------------------------------------
# needs daemon.pm (version at least 1.0.1), IO::Socket and POSIX for strftime
use daemon "1.0.1";
use IO::Socket;
use POSIX qw(strftime);

##### make your settings here:
# prot number to listen on
my $listen_port = "1025";
# listen on the followin IP address (must be undef to listen on all addrs.)
my $listen_address = undef; #"localhost";
# if you start the daemon with any argument, no detach will be done.

# start daemon
my $daemon = new daemon; 
# or if we want to change the daemon name
# my $daemon = new daemon "timeserver";

# the following lines are the defaults, so we don't need them
# $daemon -> pidfile ("/var/run/example-daemon.pid");
# $daemon -> logto ('//syslog);

# socket handler object (listen socket)
my $sock;
# connection socket
my $client;

if ($daemon -> check_running()) {
   $daemon -> kill; # kill daemon if running
} else {
   # otherwise start it (if any arguments are given, run in debug mode)
   unless ($ARGV[0]) {
      $daemon -> detach;
   } else {
      $daemon -> debug;
   }
   $daemon -> dolog("started server-daemon");

   my $sock = new IO::Socket::INET (
                                    LocalHost => $listen_address,
                                    MultiHomed => 1,
                                    LocalPort => $listen_port,
                                    Proto => 'tcp',
                                    Listen => 1,
                                    Reuse => 1,
                                   );
   $daemon->dolog ("Could not create listen socket: $!\n")
      unless $sock;

   while (($client=$sock->accept()) && $client) {
      dolog $daemon "accepted connection from ".$client->peerhost().
         " port ".$client->peerport();
      print $client (POSIX::strftime("%c",localtime))."\n";
      close ($client)
   };

   close ($sock);

   dolog $daemon "server-daemon done its work";
};

# end daemon (should not be reached)
exit 0;

