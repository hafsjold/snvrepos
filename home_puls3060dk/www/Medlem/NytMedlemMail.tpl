Hej {$medlem->fornavn},

Tak for din indmeldelse i Puls 3060.
Du har givet os f�lgende oplysninger i forbindelse med din indmeldelse:

  Ref-nr......: {$medlem->id}
  Fornavn.....: {$medlem->fornavn}
  Efternavn...: {$medlem->efternavn}
  Adresse.....: {$medlem->adresse}
  Postnummer..: {$medlem->postnr}
  By..........: {$medlem->bynavn}
  Telefon.....: {$medlem->tlfnr}
  E-mail......: {$medlem->mailadr}
  F�dselsdato.: {$medlem->fodtdato}
  K�n.........: {$medlem->kon}

Inden medlemskabet kan tr�de i kraft, skal du betale et medlemskontingent, for perioden fra {$medlem->indmeldtdato|date_format:"%A den %e. %B %G"} og frem til {$medlem->kontingenttildato|date_format:"%A den %e. %B %G"}, p� kr. {$medlem->kontingentkr},00.

Du vil inden for kort tid modtage et indbetalingskort, som skal anvendes til betaling af kontingentet.

Vi ser frem til at l�be eller power walke sammen med dig.

Med venlig hilsen
Puls 3060

Bes�g vores hjemmeside www.puls3060.dk
