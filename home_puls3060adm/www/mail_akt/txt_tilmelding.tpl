Tilmelding til {$tilmelding->klinieoverskrift} {$tilmelding->kliniedato|date_format:"%A den %e. %B %G kl %H.%M"}

Hej {$tilmelding->fornavn}

Du kan tilmelde dig elektronisk ved at trykke p� dette link https://www.puls3060.dk?p0={$tilmelding->p0} 

Efter tilmelding vil du f� tilsendt en e-mail som kvittering.

Hvis du vil vide mere om {$tilmelding->klinieoverskrift} s� tryk p� dette link http://www.puls3060.dk{$tilmelding->link}?p3=no

Med venlig hilsen
Puls 3060

Sidste frist for tilmelding er {$tilmelding->tilmeldingslut|date_format:"%A den %e. %B"}.
