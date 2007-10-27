#!/usr/bin/perl -w

my $medlem = "Hans Olsen";
my $mypic = "
Kære @<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<.


Så vidt jeg kan se, har du ikke betalt seneste kontingentopkrævning for medlemskabet af Løbeklubben PULS3060.

Hvis det skyldes en forglemmelse, bedes du foretage indbetalingen snarest.

Har du ikke modtaget opkrævningen, eller er den bortkommet, så kontakte mig venligst.


På foreningens vegne
Mogens Hafsjold

";

formline $mypic, $medlem;

print $^A;
