Hej {$tilmelding->fornavn},

Tak for din tilmelding til {$tilmelding->aktnavn} {$tilmelding->aktdato|date_format:"%A den %e. %B"}.

Vi har registreret f�lgende oplysninger i forbindelse med din tilmelding:

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

Deltagerafgiften p� {$tilmelding->aktafgift|string_format:"%d DKK"} vil blive h�vet p� dit Dankort dagen efter tilmeldingsfristens udl�b. 

Med venlig hilsen
Puls 3060

Bes�g vores hjemmeside www.puls3060.dk
