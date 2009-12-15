ALTER TABLE [dicMedlem] DROP CONSTRAINT [Reference];

ALTER TABLE [tblbet] DROP CONSTRAINT [tblfrapbstblbet];

ALTER TABLE [tblbetlin] DROP CONSTRAINT [tblbettblbetlin];

ALTER TABLE [tblfak] DROP CONSTRAINT [tbltilpbstblfak];

ALTER TABLE [tblfrapbs] DROP CONSTRAINT [tblPBSForsendelsetblfrapbs];

ALTER TABLE [tblMedlemLog] DROP CONSTRAINT [tblMedlemtblMedlemLog];

ALTER TABLE [tblpbsfile] DROP CONSTRAINT [tblpbsfilestblpbsfile];

ALTER TABLE [tblpbsfiles] DROP CONSTRAINT [tblPBSForsendelsetblpbsfiles];

ALTER TABLE [tblRegnskab] DROP CONSTRAINT [tblAktivtRegnskabtblRegnskab];

ALTER TABLE [tblSyncMedlem] DROP CONSTRAINT [dicMedlemtblSyncMedlem];

ALTER TABLE [tblSyncMedlem] DROP CONSTRAINT [tblMedlemtblSyncMedlem];

ALTER TABLE [tbltilpbs] DROP CONSTRAINT [tblPBSForsendelsetbltilpbs];

DROP TABLE [dicMedlem];

DROP TABLE [syncMedlem];

DROP TABLE [tblAktivitet];

DROP TABLE [tblAktivtRegnskab];

DROP TABLE [tblApplication];

DROP TABLE [tblbet];

DROP TABLE [tblbetlin];

DROP TABLE [tblfak];

DROP TABLE [tblfrapbs];

DROP TABLE [tblkreditor];

DROP TABLE [tblMedlem];

DROP TABLE [tblMedlemLog];

DROP TABLE [tblnrserie];

DROP TABLE [tblpbsfile];

DROP TABLE [tblpbsfiles];

DROP TABLE [tblpbsforsendelse];

DROP TABLE [tblpbsnetdir];

DROP TABLE [tblRegnskab];

DROP TABLE [tblSyncMedlem];

DROP TABLE [tbltilpbs];

DROP TABLE [tempDkkonti];

DROP TABLE [tempFields];

DROP TABLE [tempKartotek];

DROP TABLE [tempKortnr];

DROP TABLE [tempStatus];

