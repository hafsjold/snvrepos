Hej {$tilmelding->fornavn},

Tak for din tilmelding til {$tilmelding->lobnavn} {$tilmelding->lobdato|date_format:"%A den %e. %B"}.

Vi har registreret følgende oplysninger i forbindelse med din tilmelding:

  Løb.........: {$tilmelding->lobnavn}
  Afdeling....: {$tilmelding->afdnavn}
  Løbsdag.....: {$tilmelding->lobdato|date_format:"%A den %e. %B %G kl %H.%M"}
  Fornavn.....: {$tilmelding->fornavn}
  Efternavn...: {$tilmelding->efternavn}
  Adresse.....: {$tilmelding->adresse}
  Postnummer..: {$tilmelding->postnr}
  By..........: {$tilmelding->bynavn}
  Telefon.....: {$tilmelding->tlfnr}
  E-mail......: {$tilmelding->mailadr}
  FødselsÅr...: {$tilmelding->fodtaar}
  Køn.........: {$tilmelding->kon}
  Løbsnummer..: {$tilmelding->nummer}
  Løbsafgift..: {$tilmelding->lobsafgift|string_format:"%d DKK"}
  Ordrenummer.: {$tilmelding->ordernum}

{$tilmelding->afhente_dit_loebsnummer}

Løbsafgiften på {$tilmelding->lobsafgift|string_format:"%d DKK"} vil blive hævet på dit Dankort dagen efter løbet. 
Hvis du mod forventning skulle blive forhindret i at deltage i løbet vil vi ikke hæve løbsafgiften 
fra dit Dankort, og du har derfor ikke haft nogen udgift i forbindelse med denne tilmelding.

Med venlig hilsen
Puls 3060

Besøg vores hjemmeside www.puls3060.dk
