#!/usr/bin/perl -w

my $medlem = "Hans Olsen";
my $mypic = "
K�re @<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<.


S� vidt jeg kan se, har du ikke betalt seneste kontingentopkr�vning for medlemskabet af L�beklubben PULS3060.

Hvis det skyldes en forglemmelse, bedes du foretage indbetalingen snarest.

Har du ikke modtaget opkr�vningen, eller er den bortkommet, s� kontakte mig venligst.


P� foreningens vegne
Mogens Hafsjold

";

formline $mypic, $medlem;

print $^A;
