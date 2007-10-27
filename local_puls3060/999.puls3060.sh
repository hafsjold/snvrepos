#!/bin/sh

PREFIX=/usr/local
PULS3060BIN=${PREFIX}/puls3060

case $1 in
start)
    if [ -e /var/run/puls3060-daemon.pid ]; then
      echo "start: `basename $0` is allready running"
    else
      [ -x ${PULS3060BIN}/puls3060-daemon.pl ] && {
	  echo -n ' puls3060'
	  su -l root -c \
	      "exec ${PULS3060BIN}/puls3060-daemon.pl"
      }
	fi
    ;;

stop)
    if [ -e /var/run/puls3060-daemon.pid ]; then
      [ -x ${PULS3060BIN}/puls3060-daemon.pl ] && {
	  echo -n ' puls3060'
	  su -l root -c \
	      "exec ${PULS3060BIN}/puls3060-daemon.pl"
      }
    else
      echo "stop: `basename $0` is not running"
	fi
    ;;

status)
    if [ -e /var/run/puls3060-daemon.pid ]; then
      echo "status: `basename $0` is running"
    else
      echo "status: `basename $0` is not running"
	fi
    ;;

*)
    echo "usage: `basename $0` {start|stop|status}" >&2
    exit 64
    ;;
esac
