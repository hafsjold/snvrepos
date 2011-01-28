CREATE TABLE [tblPerson] (
  [Nr] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE
, [navn] nvarchar NOT NULL
, [kaldenavn] nvarchar DEFAULT NULL
, [adresse] nvarchar DEFAULT NULL
, [postnr] nvarchar DEFAULT NULL
, [bynavn] nvarchar DEFAULT NULL
, [email] nvarchar DEFAULT NULL
, [telefon] nvarchar DEFAULT NULL
, [kon] nvarchar(1) DEFAULT NULL
, [fodtdato] date DEFAULT NULL
, [bank] nvarchar DEFAULT NULL
, [medlemtildato]  date DEFAULT NULL
, [medlemaabenbetalingsdato]  date DEFAULT NULL
, [key] nvarchar DEFAULT NULL
);
CREATE UNIQUE INDEX [UQ__tblPerson__000000000000E23D] ON [tblPerson] ([Nr] ASC);

CREATE TABLE [tblpbsforsendelse] (
  [id] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE AUTOINCREMENT
, [delsystem] nvarchar(3) NOT NULL
, [leverancetype] nvarchar(4) DEFAULT NULL
, [oprettetaf] nvarchar(3) DEFAULT NULL
, [oprettet] datetime DEFAULT NULL
, [leveranceid] int DEFAULT NULL
, [key] nvarchar DEFAULT NULL
);
CREATE UNIQUE INDEX [UQ__tblpbsforsendelse__000000000000023D] ON [tblpbsforsendelse] ([id] ASC);

CREATE TABLE [tbltilpbs] (
  [id] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE AUTOINCREMENT
, [delsystem] nvarchar(3) DEFAULT NULL
, [leverancetype] nvarchar(4) DEFAULT NULL
, [bilagdato] datetime DEFAULT NULL
, [pbsforsendelseid] int DEFAULT NULL
, [udtrukket] datetime DEFAULT NULL
, [leverancespecifikation] nvarchar(10) DEFAULT NULL
, [leverancedannelsesdato] datetime DEFAULT NULL
, [key] nvarchar DEFAULT NULL
, CONSTRAINT [FK_tblPBSForsendelse_tbltilpbs] FOREIGN KEY ([pbsforsendelseid]) REFERENCES [tblpbsforsendelse]([id]) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE UNIQUE INDEX [UQ__tbltilpbs__0000000000000265] ON [tbltilpbs] ([id] ASC);

CREATE TABLE [tblfak] (
  [id] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE AUTOINCREMENT
, [tilpbsid] int DEFAULT NULL
, [betalingsdato] datetime DEFAULT NULL
, [Nr] int DEFAULT NULL
, [faknr] int DEFAULT NULL
, [advistekst] nvarchar(4000) DEFAULT NULL
, [advisbelob] numeric(18,2) DEFAULT NULL
, [infotekst] int DEFAULT NULL
, [bogfkonto] int DEFAULT NULL
, [vnr] int DEFAULT NULL
, [fradato] datetime DEFAULT NULL
, [tildato] datetime DEFAULT NULL
, [SFakID] int DEFAULT NULL
, [SFaknr] int DEFAULT NULL
, [rykkerdato] datetime DEFAULT 0
, [maildato] datetime DEFAULT 0
, [rykkerstop] bit NOT NULL DEFAULT 0
, [betalt] bit NOT NULL DEFAULT 0
, [tilmeldtpbs] bit NOT NULL DEFAULT 0
, [indmeldelse] bit NOT NULL DEFAULT 0
, [key] nvarchar DEFAULT NULL
, CONSTRAINT [FK_tbltilpbs_tblfak] FOREIGN KEY ([tilpbsid]) REFERENCES [tbltilpbs]([id]) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE UNIQUE INDEX [UQ__tblfak__00000000000001E3] ON [tblfak] ([id] ASC);
CREATE INDEX [medlem_fak] ON [tblfak] ([Nr] ASC,[faknr] ASC);

CREATE TABLE [tbloverforsel] (
  [id] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE AUTOINCREMENT
, [tilpbsid] int DEFAULT NULL
, [Nr] int DEFAULT NULL
, [SFaknr] int DEFAULT NULL
, [SFakID] int DEFAULT NULL
, [advistekst] nvarchar(20) DEFAULT NULL
, [advisbelob] numeric(18,2) DEFAULT NULL
, [emailtekst] nvarchar(4000) DEFAULT NULL
, [emailsent] bit DEFAULT 0
, [bankregnr] nvarchar(4) DEFAULT NULL
, [bankkontonr] nvarchar(10) DEFAULT NULL
, [betalingsdato] datetime DEFAULT NULL
, [key] nvarchar DEFAULT NULL
, CONSTRAINT [FK_tbltilpbs_tbloverfoersel] FOREIGN KEY ([tilpbsid]) REFERENCES [tbltilpbs]([id]) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE UNIQUE INDEX [UQ__tbloverforsel__00000000000006B2] ON [tbloverforsel] ([id] ASC);

CREATE TABLE [tblrykker] (
  [id] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE AUTOINCREMENT
, [tilpbsid] int DEFAULT NULL
, [betalingsdato] datetime DEFAULT NULL
, [Nr] int DEFAULT NULL
, [faknr] int DEFAULT NULL
, [advistekst] nvarchar(4000) DEFAULT NULL
, [advisbelob] numeric(18,2) DEFAULT NULL
, [infotekst] int DEFAULT NULL
, [rykkerdato] datetime DEFAULT NULL
, [maildato] datetime DEFAULT NULL
, [key] nvarchar DEFAULT NULL
, CONSTRAINT [FK_tbltilpbs_tblrykker] FOREIGN KEY ([tilpbsid]) REFERENCES [tbltilpbs]([id]) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE UNIQUE INDEX [UQ__tblrykker__00000000000001E7] ON [tblrykker] ([id] ASC);
CREATE INDEX [tblmedlem_tblrykker] ON [tblrykker] ([Nr] ASC,[faknr] ASC);

CREATE TABLE [tblpbsfiles] (
  [id] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE AUTOINCREMENT
, [type] int DEFAULT NULL
, [path] nvarchar(255) DEFAULT NULL
, [filename] nvarchar(50) DEFAULT NULL
, [size] int DEFAULT NULL
, [atime] datetime DEFAULT NULL
, [mtime] datetime DEFAULT NULL
, [perm] nvarchar(50) DEFAULT NULL
, [uid] int DEFAULT NULL
, [gid] int DEFAULT NULL
, [transmittime] datetime DEFAULT NULL
, [pbsforsendelseid] int DEFAULT NULL
, [key] nvarchar DEFAULT NULL
, CONSTRAINT [pbsforsendelse_tblpbsfiles] FOREIGN KEY ([pbsforsendelseid]) REFERENCES [tblpbsforsendelse]([id]) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE UNIQUE INDEX [UQ__tblpbsfiles__0000000000000233] ON [tblpbsfiles] ([id] ASC);

CREATE TABLE [tblpbsfile] (
  [id] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE AUTOINCREMENT
, [pbsfilesid] int NOT NULL
, [data] nvarchar DEFAULT NULL
, [key] nvarchar DEFAULT NULL
, CONSTRAINT [FK_tblpbsfiles_tblpbsfile] FOREIGN KEY ([pbsfilesid]) REFERENCES [tblpbsfiles]([id]) ON DELETE CASCADE ON UPDATE CASCADE

);
CREATE UNIQUE INDEX [UQ__tblpbsfile__0000000000000229] ON [tblpbsfile] ([id] ASC);

CREATE TABLE [tblfrapbs] (
  [id] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE AUTOINCREMENT
, [delsystem] nvarchar(3) DEFAULT NULL
, [leverancetype] nvarchar(4) DEFAULT NULL
, [udtrukket] datetime DEFAULT NULL
, [bilagdato] datetime DEFAULT NULL
, [pbsforsendelseid] int DEFAULT NULL
, [leverancespecifikation] nvarchar(50) DEFAULT NULL
, [leverancedannelsesdato] datetime DEFAULT NULL
, [key] nvarchar DEFAULT NULL
, CONSTRAINT [FK_tblPBSForsendelse_tblfrapbs] FOREIGN KEY ([pbsforsendelseid]) REFERENCES [tblpbsforsendelse]([id]) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE UNIQUE INDEX [UQ__tblfrapbs__00000000000001ED] ON [tblfrapbs] ([id] ASC);

CREATE TABLE [tblbet] (
  [id] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE AUTOINCREMENT
, [frapbsid] int DEFAULT NULL
, [pbssektionnr] nvarchar(4) DEFAULT NULL
, [transkode] nvarchar(4) DEFAULT NULL
, [bogforingsdato] datetime DEFAULT NULL
, [indbetalingsbelob] numeric(18,2) DEFAULT NULL
, [summabogfort] bit DEFAULT 0
, [key] nvarchar DEFAULT NULL
, CONSTRAINT [FK_tblfrapbs_tblbet] FOREIGN KEY ([frapbsid]) REFERENCES [tblfrapbs]([id]) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE UNIQUE INDEX [UQ__tblbet__00000000000001CF] ON [tblbet] ([id] ASC);

CREATE TABLE [tblbetlin] (
  [id] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE AUTOINCREMENT
, [betid] int DEFAULT NULL
, [pbssektionnr] nvarchar(4) DEFAULT NULL
, [pbstranskode] nvarchar(4) DEFAULT NULL
, [Nr] int DEFAULT NULL
, [faknr] int DEFAULT NULL
, [debitorkonto] nvarchar(15) DEFAULT NULL
, [aftalenr] int DEFAULT NULL
, [betalingsdato] datetime DEFAULT NULL
, [belob] numeric(18,2) DEFAULT NULL
, [indbetalingsdato] datetime DEFAULT NULL
, [bogforingsdato] datetime DEFAULT NULL
, [indbetalingsbelob] numeric(18,2) DEFAULT NULL
, [pbskortart] nvarchar(2) DEFAULT NULL
, [pbsgebyrbelob] numeric(18,2) DEFAULT NULL
, [pbsarkivnr] nvarchar(22) DEFAULT NULL
, [key] nvarchar DEFAULT NULL
, CONSTRAINT [FK_tblbet_tblbetlin] FOREIGN KEY ([betid]) REFERENCES [tblbet]([id]) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE UNIQUE INDEX [UQ__tblbetlin__00000000000001D9] ON [tblbetlin] ([id] ASC);
CREATE INDEX [medlem_betaling] ON [tblbetlin] ([Nr] ASC,[faknr] ASC);

CREATE TABLE [tblaftalelin] (
  [id] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE AUTOINCREMENT
, [frapbsid] int NOT NULL
, [pbstranskode] nvarchar(4) NOT NULL
, [Nr] int NOT NULL
, [debitorkonto] nvarchar(15) NOT NULL
, [debgrpnr] nvarchar(5) NOT NULL
, [aftalenr] int DEFAULT NULL
, [aftalestartdato] datetime DEFAULT NULL
, [aftaleslutdato] datetime DEFAULT NULL
, [pbssektionnr] nvarchar(5) NOT NULL
, [key] nvarchar DEFAULT NULL
, CONSTRAINT [FK_tblfrapbs_tblaftalelin] FOREIGN KEY ([frapbsid]) REFERENCES [tblfrapbs]([id]) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE UNIQUE INDEX [UQ__tblaftalelin__00000000000011D9] ON [tblaftalelin] ([id] ASC);

CREATE TABLE [tblindbetalingskort] (
  [id] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE AUTOINCREMENT
, [frapbsid] int NOT NULL
, [pbstranskode] nvarchar(4) NOT NULL
, [Nr] int NOT NULL
, [faknr] int DEFAULT NULL
, [debitorkonto] nvarchar(15) NOT NULL
, [debgrpnr] nvarchar(5) NOT NULL
, [kortartkode] nvarchar(2) NOT NULL
, [fikreditornr] nvarchar(8) NOT NULL
, [indbetalerident] nvarchar(19) NOT NULL
, [dato] datetime DEFAULT NULL
, [belob] numeric(18,2) DEFAULT NULL
, [pbssektionnr] nvarchar(5) NOT NULL
, [key] nvarchar DEFAULT NULL
, CONSTRAINT [FK_tblfrapbs_tblindbetalingskort] FOREIGN KEY ([frapbsid]) REFERENCES [tblfrapbs]([id]) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE UNIQUE INDEX [UQ__tblindbetalingskort__00000000000D11D9] ON [tblindbetalingskort] ([id] ASC);

CREATE TABLE [tblAktivitet] (
  [id] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE
, [akt_tekst] nvarchar(30) NOT NULL
, [key] nvarchar DEFAULT NULL
);
CREATE UNIQUE INDEX [UQ__tblAktivitet__00000000000001B1] ON [tblAktivitet] ([id] ASC);

CREATE TABLE [tblinfotekst] (
  [id] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE
, [navn] nvarchar(50) DEFAULT NULL
, [msgtext] nvarchar(4000) DEFAULT NULL
, [key] nvarchar DEFAULT NULL
);
CREATE UNIQUE INDEX [UQ__tblinfotekst__0000000000000230] ON [tblinfotekst] ([id] ASC);

CREATE TABLE [tblkreditor] (
  [id] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE
, [datalevnr] nvarchar(8) NOT NULL
, [datalevnavn] nvarchar(15) NOT NULL
, [pbsnr] nvarchar(8) NOT NULL
, [delsystem] nvarchar(3) DEFAULT NULL
, [regnr] nvarchar(4) NOT NULL
, [kontonr] nvarchar(10) NOT NULL
, [debgrpnr] nvarchar(5) NOT NULL
, [sektionnr] nvarchar(4) NOT NULL
, [transkodebetaling] nvarchar(4) NOT NULL
, [key] nvarchar DEFAULT NULL
);
CREATE UNIQUE INDEX [UQ__tblkreditor__0000000000000201] ON [tblkreditor] ([id] ASC);

CREATE TABLE [tblMedlem] (
  [Nr] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE
, [Kon] nvarchar(1) DEFAULT NULL
, [FodtDato] datetime DEFAULT NULL
, [key] nvarchar DEFAULT NULL
);
CREATE UNIQUE INDEX [UQ__tblMedlem__000000000000020B] ON [tblMedlem] ([Nr] ASC);

CREATE TABLE [tblMedlemLog] (
  [id] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE
, [Nr] int DEFAULT NULL
, [logdato] datetime DEFAULT NULL
, [akt_id] int DEFAULT NULL
, [akt_dato] datetime DEFAULT NULL
, [key] nvarchar DEFAULT NULL
, CONSTRAINT [FK_Log_Medlem] FOREIGN KEY ([Nr]) REFERENCES [tblMedlem]([Nr])
, CONSTRAINT [FK_tblAktivitet_Log] FOREIGN KEY ([akt_id]) REFERENCES [tblAktivitet]([id])
);
CREATE INDEX [tblMedlemLog_ix1] ON [tblMedlemLog] ([Nr] ASC);
CREATE UNIQUE INDEX [UQ__tblMedlemLog__0000000000000215] ON [tblMedlemLog] ([id] ASC);

CREATE TABLE [tblnrserie] (
  [nrserienavn] nvarchar(30) NOT NULL PRIMARY KEY ON CONFLICT REPLACE
, [sidstbrugtenr] int DEFAULT NULL
, [key] nvarchar DEFAULT NULL
);
CREATE UNIQUE INDEX [UQ__tblnrserie__000000000000021F] ON [tblnrserie] ([nrserienavn] ASC);

CREATE TABLE [tblRegnskab] (
  [rid] int NOT NULL PRIMARY KEY ON CONFLICT REPLACE
, [Navn] nvarchar(50) DEFAULT NULL
, [Oprettet] datetime DEFAULT NULL
, [Start] datetime DEFAULT NULL
, [Slut] datetime DEFAULT NULL
, [DatoLaas] datetime DEFAULT NULL
, [Firmanavn] nvarchar(50) DEFAULT NULL
, [Placering] nvarchar(255) DEFAULT NULL
, [Eksportmappe] nvarchar(255) DEFAULT NULL
, [TilPBS] nvarchar(255) DEFAULT NULL
, [FraPBS] nvarchar(255) DEFAULT NULL
, [Afsluttet] bit DEFAULT 0
, [key] nvarchar DEFAULT NULL
);
CREATE UNIQUE INDEX [UQ__tblRegnskab__0000000000000251] ON [tblRegnskab] ([rid] ASC);

CREATE TABLE [tblsftp] (
  [id] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE AUTOINCREMENT
, [navn] nvarchar(64) NOT NULL
, [host] nvarchar(64) NOT NULL
, [port] nvarchar(5) NOT NULL
, [user] nvarchar(16) NOT NULL
, [outbound] nvarchar(64) DEFAULT NULL
, [inbound] nvarchar(64) DEFAULT NULL
, [pincode] nvarchar(64) DEFAULT NULL
, [certificate] nvarchar(4000) DEFAULT NULL
, [key] nvarchar DEFAULT NULL
);
CREATE UNIQUE INDEX [UQ__tblsftp__00000000000006E4] ON [tblsftp] ([id] ASC);

CREATE TABLE [tblSysinfo] (
  [vkey] nvarchar(10) NOT NULL
, [val] nvarchar(100) NOT NULL
, [id] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE AUTOINCREMENT
, [key] nvarchar DEFAULT NULL
);
CREATE UNIQUE INDEX [UQ__tblSysinfo__000000000000068A] ON [tblSysinfo] ([vkey] ASC);

CREATE TABLE [tblpbsnetdir] (
  [id] INTEGER NOT NULL PRIMARY KEY ON CONFLICT REPLACE AUTOINCREMENT
, [type] int DEFAULT NULL
, [path] nvarchar(255) DEFAULT NULL
, [filename] nvarchar(50) DEFAULT NULL
, [size] int DEFAULT NULL
, [atime] datetime DEFAULT NULL
, [mtime] datetime DEFAULT NULL
, [perm] nvarchar(50) DEFAULT NULL
, [uid] int DEFAULT NULL
, [gid] int DEFAULT NULL
, [key] nvarchar DEFAULT NULL
);
CREATE UNIQUE INDEX [UQ__tblpbsnetdir__0000000000000433] ON [tblpbsnetdir] ([id] ASC);
