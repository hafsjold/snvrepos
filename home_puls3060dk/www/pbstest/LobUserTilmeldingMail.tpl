Hej {$tilmelding->fornavn},

Tak for din tilmelding til {$tilmelding->lobnavn} {$tilmelding->lobdato|date_format:"%A den %e. %B"}.

Vi har registreret f�lgende oplysninger i forbindelse med din tilmelding:

  L�b.........: {$tilmelding->lobnavn}
  Afdeling....: {$tilmelding->afdnavn}
  L�bsdag.....: {$tilmelding->lobdato|date_format:"%A den %e. %B %G kl %H.%M"}
  Fornavn.....: {$tilmelding->fornavn}
  Efternavn...: {$tilmelding->efternavn}
  Adresse.....: {$tilmelding->adresse}
  Postnummer..: {$tilmelding->postnr}
  By..........: {$tilmelding->bynavn}
  Telefon.....: {$tilmelding->tlfnr}
  E-mail......: {$tilmelding->mailadr}
  F�dsels�r...: {$tilmelding->fodtaar}
  K�n.........: {$tilmelding->kon}
  L�bsnummer..: {$tilmelding->nummer}
  L�bsafgift..: {$tilmelding->lobsafgift|string_format:"%d DKK"}
  Ordrenummer.: {$tilmelding->ordernum}

{$tilmelding->afhente_dit_loebsnummer}

L�bsafgiften p� {$tilmelding->lobsafgift|string_format:"%d DKK"} vil blive h�vet p� dit Dankort dagen efter l�bet. 
Hvis du mod forventning skulle blive forhindret i at deltage i l�bet vil vi ikke h�ve l�bsafgiften 
fra dit Dankort, og du har derfor ikke haft nogen udgift i forbindelse med denne tilmelding.

Med venlig hilsen
Puls 3060

Bes�g vores hjemmeside www.puls3060.dk
