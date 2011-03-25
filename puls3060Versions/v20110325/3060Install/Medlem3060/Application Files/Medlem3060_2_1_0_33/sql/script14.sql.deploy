ALTER TABLE [tblfak] ADD COLUMN [tilmeldtpbs] bit NULL;
GO
ALTER TABLE [tblfak] ADD COLUMN [indmeldelse] bit NULL;
GO
UPDATE [tblfak] SET [tilmeldtpbs] = 0 ,[indmeldelse] = 0;
GO
UPDATE [tblfak] SET [indmeldelse] = 1 WHERE fradato != '2010-01-01';
GO
ALTER TABLE [tblfak] ALTER COLUMN [tilmeldtpbs] bit NOT NULL;
GO
ALTER TABLE [tblfak] ALTER COLUMN [indmeldelse] bit NOT NULL;
GO
ALTER TABLE [tblfak] ALTER COLUMN [tilmeldtpbs] SET DEFAULT 0;
GO
ALTER TABLE [tblfak] ALTER COLUMN [indmeldelse] SET DEFAULT 0;
GO

ALTER TABLE [tempKontforslaglinie] ADD COLUMN [tilmeldtpbs] bit NULL;
GO
ALTER TABLE [tempKontforslaglinie] ADD COLUMN [indmeldelse] bit NULL;
GO
UPDATE [tempKontforslaglinie] SET [tilmeldtpbs] = 0 ,[indmeldelse] = 0;
GO
ALTER TABLE [tempKontforslaglinie] ALTER COLUMN [tilmeldtpbs] bit NOT NULL;
GO
ALTER TABLE [tempKontforslaglinie] ALTER COLUMN [indmeldelse] bit NOT NULL;
GO
ALTER TABLE [tempKontforslaglinie] ALTER COLUMN [tilmeldtpbs] SET DEFAULT 0;
GO
ALTER TABLE [tempKontforslaglinie] ALTER COLUMN [indmeldelse] SET DEFAULT 0;
GO