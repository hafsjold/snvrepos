CREATE TABLE [dicMedlem] (
[id] int IDENTITY(38,1) NOT NULL ,
[Name] nvarchar(50) ,
[onSumma] bit ,
[onAccess] bit ,
[onFMS_PBS] bit ,
[ConflictPrimary] tinyint ,
[accName] nvarchar(50) ,
[accNumber] tinyint ,
[sumNumber] tinyint ,
[sumSubNumber] tinyint ,
[pbsNumber] tinyint ,
[NewValue] nvarchar(50) ,
[NewType] nvarchar(50) );

CREATE TABLE [syncMedlem] (
[Nr] smallint NOT NULL ,
[crcMedlem] int ,
[crcNavn] int ,
[crcKaldenavn] int ,
[crcAdresse] int ,
[crcPostnr] int ,
[crcBynavn] int ,
[crcTelefon] int ,
[crcEmail] int );

CREATE TABLE [tblAktivitet] (
[id] tinyint NOT NULL ,
[akt_tekst] nvarchar(30) );

CREATE TABLE [tblAktivtRegnskab] (
[rid] int NOT NULL );

CREATE TABLE [tblApplication] (
[id] tinyint NOT NULL ,
[Name] nvarchar(12) NOT NULL );

CREATE TABLE [tblbet] (
[id] int NOT NULL ,
[frapbsid] int ,
[pbssektionnr] nvarchar(4) ,
[transkode] nvarchar(4) ,
[bogforingsdato] datetime ,
[indbetalingsbelob] numeric(18,2) );

CREATE TABLE [tblbetlin] (
[id] int IDENTITY(129,1) NOT NULL ,
[betid] int ,
[pbssektionnr] nvarchar(4) ,
[pbstranskode] nvarchar(4) ,
[Nr] int ,
[faknr] int ,
[debitorkonto] nvarchar(15) ,
[aftalenr] int ,
[betalingsdato] datetime ,
[belob] numeric(18,2) ,
[indbetalingsdato] datetime ,
[bogforingsdato] datetime ,
[indbetalingsbelob] numeric(18,2) ,
[pbskortart] nvarchar(2) ,
[pbsgebyrbelob] numeric(18,2) ,
[pbsarkivnr] nvarchar(22) );

CREATE TABLE [tblfak] (
[id] int NOT NULL ,
[tilpbsid] int ,
[betalingsdato] datetime ,
[Nr] int ,
[faknr] int ,
[advistekst] ntext ,
[advisbelob] numeric(18,2) ,
[infotekst] int ,
[bogfkonto] int ,
[vnr] smallint ,
[fradato] datetime ,
[tildato] datetime ,
[SFakID] smallint ,
[SFaknr] smallint ,
[rykkerdato] datetime ,
[maildato] datetime ,
[rykkerstop] bit ,
[betalt] bit );

CREATE TABLE [tblfrapbs] (
[id] int NOT NULL ,
[delsystem] nvarchar(3) ,
[leverancetype] nvarchar(4) ,
[udtrukket] datetime ,
[bilagdato] datetime ,
[pbsforsendelseid] int ,
[leverancespecifikation] nvarchar(50) ,
[leverancedannelsesdato] datetime );

CREATE TABLE [tblkreditor] (
[id] int IDENTITY(5,1) NOT NULL ,
[datalevnr] nvarchar(8) NOT NULL ,
[datalevnavn] nvarchar(15) NOT NULL ,
[pbsnr] nvarchar(8) NOT NULL ,
[delsystem] nvarchar(3) ,
[regnr] nvarchar(4) NOT NULL ,
[kontonr] nvarchar(10) NOT NULL ,
[debgrpnr] nvarchar(5) NOT NULL ,
[sektionnr] nvarchar(4) NOT NULL ,
[transkodebetaling] nvarchar(4) NOT NULL );

CREATE TABLE [tblMedlem] (
[Nr] smallint NOT NULL ,
[Navn] nvarchar(40) ,
[Kaldenavn] nvarchar(40) ,
[Adresse] nvarchar(40) ,
[Postnr] smallint ,
[Bynavn] nvarchar(40) ,
[Telefon] nvarchar(12) ,
[Email] nvarchar(100) ,
[Knr] smallint ,
[Kon] nvarchar(1) ,
[FodtDato] datetime );

CREATE TABLE [tblMedlemLog] (
[id] int IDENTITY(7,1) NOT NULL ,
[Nr] smallint ,
[logdato] datetime ,
[akt_id] tinyint ,
[akt_dato] datetime );

CREATE TABLE [tblnrserie] (
[nrserienavn] nvarchar(30) NOT NULL ,
[sidstbrugtenr] int );

CREATE TABLE [tblpbsfile] (
[id] int IDENTITY(283,1) NOT NULL ,
[pbsfilesid] int NOT NULL ,
[seqnr] int NOT NULL ,
[data] nvarchar(128) );

CREATE TABLE [tblpbsfiles] (
[id] int IDENTITY(733,1) NOT NULL ,
[type] tinyint ,
[path] nvarchar(255) ,
[filename] nvarchar(50) ,
[size] int ,
[atime] datetime ,
[mtime] datetime ,
[perm] nvarchar(50) ,
[uid] int ,
[gid] int ,
[transmittime] datetime ,
[pbsforsendelseid] int );

CREATE TABLE [tblpbsforsendelse] (
[id] int NOT NULL ,
[delsystem] nvarchar(3) NOT NULL ,
[leverancetype] nvarchar(4) ,
[oprettetaf] nvarchar(3) ,
[oprettet] datetime ,
[leveranceid] int );

CREATE TABLE [tblpbsnetdir] (
[id] int IDENTITY(148,1) NOT NULL ,
[type] tinyint ,
[path] nvarchar(255) ,
[filename] nvarchar(50) ,
[size] int ,
[atime] datetime ,
[mtime] datetime ,
[perm] nvarchar(50) ,
[uid] int ,
[gid] int );

CREATE TABLE [tblRegnskab] (
[rid] int NOT NULL ,
[Navn] nvarchar(50) ,
[Oprettet] datetime ,
[Start] datetime ,
[Slut] datetime ,
[DatoLaas] datetime ,
[Firmanavn] nvarchar(50) ,
[Placering] nvarchar(255) ,
[Eksportmappe] nvarchar(255) ,
[TilPBS] nvarchar(255) ,
[FraPBS] nvarchar(255) );

CREATE TABLE [tblSyncMedlem] (
[id] int IDENTITY(2276,1) NOT NULL ,
[Nr] smallint NOT NULL ,
[dicid] int NOT NULL ,
[crc] int );

CREATE TABLE [tbltilpbs] (
[id] int NOT NULL ,
[delsystem] nvarchar(3) ,
[leverancetype] nvarchar(4) ,
[bilagdato] datetime ,
[pbsforsendelseid] int ,
[udtrukket] datetime ,
[leverancespecifikation] nvarchar(10) ,
[leverancedannelsesdato] datetime );

CREATE TABLE [tempDkkonti] (
[Debnr] nvarchar(7) NOT NULL );

CREATE TABLE [tempFields] (
[Nr] smallint NOT NULL ,
[fldnr] smallint NOT NULL ,
[fldsubnr] tinyint NOT NULL ,
[value] nvarchar(255) );

CREATE TABLE [tempKartotek] (
[Nr] smallint NOT NULL ,
[synced] bit NOT NULL ,
[csvFromFile] ntext ,
[csvToFile] ntext ,
[tNr] nvarchar(4) ,
[Debnr] nvarchar(7) );

CREATE TABLE [tempKortnr] (
[Nr] nvarchar(4) NOT NULL );

CREATE TABLE [tempStatus] (
[key] nvarchar(20) NOT NULL ,
[value] nvarchar(255) );

ALTER TABLE [dicMedlem] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([id]);

CREATE INDEX [id] ON [dicMedlem] ([id] ASC);

ALTER TABLE [syncMedlem] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([Nr]);

ALTER TABLE [tblAktivitet] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([id]);

CREATE INDEX [id] ON [tblAktivitet] ([id] ASC);

ALTER TABLE [tblAktivtRegnskab] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([rid]);

CREATE UNIQUE INDEX [rid] ON [tblAktivtRegnskab] ([rid] ASC);

ALTER TABLE [tblApplication] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([id]);

CREATE INDEX [id] ON [tblApplication] ([id] ASC);

ALTER TABLE [tblbet] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([id]);

CREATE INDEX [frapbsid] ON [tblbet] ([frapbsid] ASC);

CREATE INDEX [id] ON [tblbet] ([id] ASC);

ALTER TABLE [tblbetlin] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([id]);

CREATE INDEX [betid] ON [tblbetlin] ([betid] ASC);

CREATE INDEX [id] ON [tblbetlin] ([id] ASC);

ALTER TABLE [tblfak] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([id]);

CREATE INDEX [id] ON [tblfak] ([id] ASC);

CREATE INDEX [SFakID] ON [tblfak] ([SFakID] ASC);

CREATE INDEX [tilpbsid] ON [tblfak] ([tilpbsid] ASC);

ALTER TABLE [tblfrapbs] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([id]);

CREATE INDEX [id] ON [tblfrapbs] ([id] ASC);

CREATE INDEX [pbsforsendelseid] ON [tblfrapbs] ([pbsforsendelseid] ASC);

ALTER TABLE [tblkreditor] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([id]);

CREATE INDEX [id] ON [tblkreditor] ([id] ASC);

ALTER TABLE [tblMedlem] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([Nr]);

ALTER TABLE [tblMedlemLog] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([id]);

CREATE INDEX [akt_id] ON [tblMedlemLog] ([akt_id] ASC);

CREATE INDEX [id] ON [tblMedlemLog] ([id] ASC);

CREATE INDEX [tblMedlemLogNr] ON [tblMedlemLog] ([Nr] ASC);

ALTER TABLE [tblnrserie] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([nrserienavn]);

ALTER TABLE [tblpbsfile] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([id]);

CREATE INDEX [id] ON [tblpbsfile] ([id] ASC);

CREATE INDEX [pbsfilesid] ON [tblpbsfile] ([pbsfilesid] ASC);

CREATE INDEX [seqnr] ON [tblpbsfile] ([seqnr] ASC);

ALTER TABLE [tblpbsfiles] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([id]);

CREATE INDEX [gid] ON [tblpbsfiles] ([gid] ASC);

CREATE INDEX [id] ON [tblpbsfiles] ([id] ASC);

CREATE INDEX [pbsforsendelseid] ON [tblpbsfiles] ([pbsforsendelseid] ASC);

CREATE INDEX [uid] ON [tblpbsfiles] ([uid] ASC);

ALTER TABLE [tblpbsforsendelse] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([id]);

CREATE INDEX [id] ON [tblpbsforsendelse] ([id] ASC);

CREATE INDEX [leveranceid] ON [tblpbsforsendelse] ([leveranceid] ASC);

ALTER TABLE [tblpbsnetdir] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([id]);

CREATE INDEX [gid] ON [tblpbsnetdir] ([gid] ASC);

CREATE INDEX [id] ON [tblpbsnetdir] ([id] ASC);

CREATE INDEX [uid] ON [tblpbsnetdir] ([uid] ASC);

ALTER TABLE [tblRegnskab] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([rid]);

CREATE UNIQUE INDEX [rid] ON [tblRegnskab] ([rid] ASC);

ALTER TABLE [tblSyncMedlem] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([id]);

CREATE INDEX [id] ON [tblSyncMedlem] ([id] ASC);

CREATE INDEX [id_dic] ON [tblSyncMedlem] ([dicid] ASC);

CREATE INDEX [Nr] ON [tblSyncMedlem] ([Nr] ASC);

ALTER TABLE [tbltilpbs] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([id]);

CREATE UNIQUE INDEX [leverancespecifikation] ON [tbltilpbs] ([leverancespecifikation] ASC);

CREATE INDEX [id] ON [tbltilpbs] ([id] ASC);

CREATE INDEX [pbsforsendelsesid] ON [tbltilpbs] ([pbsforsendelseid] ASC);

ALTER TABLE [tempDkkonti] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([Debnr]);

ALTER TABLE [tempFields] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([Nr], [fldnr], [fldsubnr]);

ALTER TABLE [tempKartotek] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([Nr]);

ALTER TABLE [tempKortnr] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([Nr]);

ALTER TABLE [tempStatus] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([key]);

CREATE INDEX [key] ON [tempStatus] ([key] ASC);

ALTER TABLE [dicMedlem] ADD CONSTRAINT [Reference] FOREIGN KEY ([ConflictPrimary]) REFERENCES [tblApplication] ([id]) ON UPDATE NO ACTION ON DELETE NO ACTION;

ALTER TABLE [tblbet] ADD CONSTRAINT [tblfrapbstblbet] FOREIGN KEY ([frapbsid]) REFERENCES [tblfrapbs] ([id]) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE [tblbetlin] ADD CONSTRAINT [tblbettblbetlin] FOREIGN KEY ([betid]) REFERENCES [tblbet] ([id]) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE [tblfak] ADD CONSTRAINT [tbltilpbstblfak] FOREIGN KEY ([tilpbsid]) REFERENCES [tbltilpbs] ([id]) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE [tblfrapbs] ADD CONSTRAINT [tblPBSForsendelsetblfrapbs] FOREIGN KEY ([pbsforsendelseid]) REFERENCES [tblpbsforsendelse] ([id]) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE [tblMedlemLog] ADD CONSTRAINT [tblMedlemtblMedlemLog] FOREIGN KEY ([Nr]) REFERENCES [tblMedlem] ([Nr]) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE [tblpbsfile] ADD CONSTRAINT [tblpbsfilestblpbsfile] FOREIGN KEY ([pbsfilesid]) REFERENCES [tblpbsfiles] ([id]) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE [tblpbsfiles] ADD CONSTRAINT [tblPBSForsendelsetblpbsfiles] FOREIGN KEY ([pbsforsendelseid]) REFERENCES [tblpbsforsendelse] ([id]) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE [tblRegnskab] ADD CONSTRAINT [tblAktivtRegnskabtblRegnskab] FOREIGN KEY ([rid]) REFERENCES [tblAktivtRegnskab] ([rid]) ON UPDATE NO ACTION ON DELETE NO ACTION;

ALTER TABLE [tblSyncMedlem] ADD CONSTRAINT [dicMedlemtblSyncMedlem] FOREIGN KEY ([dicid]) REFERENCES [dicMedlem] ([id]) ON UPDATE NO ACTION ON DELETE NO ACTION;

ALTER TABLE [tblSyncMedlem] ADD CONSTRAINT [tblMedlemtblSyncMedlem] FOREIGN KEY ([Nr]) REFERENCES [tblMedlem] ([Nr]) ON UPDATE NO ACTION ON DELETE NO ACTION;

ALTER TABLE [tbltilpbs] ADD CONSTRAINT [tblPBSForsendelsetbltilpbs] FOREIGN KEY ([pbsforsendelseid]) REFERENCES [tblpbsforsendelse] ([id]) ON UPDATE CASCADE ON DELETE CASCADE;

