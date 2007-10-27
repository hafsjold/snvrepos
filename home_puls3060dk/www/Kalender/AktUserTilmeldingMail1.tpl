Hej {$tilmelding->fornavn},

Tak for din tilmelding til {$tilmelding->aktnavn} {$tilmelding->aktdato|date_format:"%A den %e. %B"}.

Vi har registreret følgende oplysninger i forbindelse med din tilmelding:

  Dato/Tid......: {$tilmelding->aktdato|date_format:"%A den %e. %B %G kl %H.%M"}
  Fornavn.......: {$tilmelding->fornavn}
  Efternavn.....: {$tilmelding->efternavn}
  Adresse.......: {$tilmelding->adresse}
  Postnummer....: {$tilmelding->postnr}
  By............: {$tilmelding->bynavn}
  Telefon.......: {$tilmelding->tlfnr}
  E-mail........: {$tilmelding->mailadr}
  Deltagerafgift: {$tilmelding->aktafgift|string_format:"%d DKK"}
  Ordrenummer...: {$tilmelding->ordernum}

Deltagerafgiften på {$tilmelding->aktafgift|string_format:"%d DKK"} vil blive hævet på dit Dankort dagen efter tilmeldingsfristens udløb. 

Med venlig hilsen
Puls 3060

Besøg vores hjemmeside www.puls3060.dk
