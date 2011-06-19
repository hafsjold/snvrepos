CREATE TABLE [tblsync] ([nr] int NOT NULL, [source] tinyint NOT NULL, [source_id] int NOT NULL, [field_id] tinyint NOT NULL, [value] nvarchar(255) NULL);
GO
ALTER TABLE [tblsync] ADD PRIMARY KEY ([nr],[source],[source_id],[field_id]);
GO
CREATE TABLE [tempsync] ([nr] int NOT NULL, [source] tinyint NOT NULL, [source_id] int NOT NULL, [field_id] tinyint NOT NULL, [value] nvarchar(255) NULL);
GO
ALTER TABLE [tempsync] ADD PRIMARY KEY ([nr],[source],[source_id],[field_id]);
GO
CREATE TABLE [tempsync2] ([nr] int NOT NULL, [source] tinyint NOT NULL, [source_id] int NOT NULL, [field_id] tinyint NOT NULL, [value] nvarchar(255) NULL);
GO
ALTER TABLE [tempsync2] ADD PRIMARY KEY ([nr],[source],[source_id],[field_id]);
GO
CREATE TABLE [tempimpexp] ([ie] nvarchar(1) NOT NULL,  [nr] int NOT NULL, [source] tinyint NOT NULL, [source_id] int NOT NULL, [field_id] tinyint NOT NULL, [value] nvarchar(255) NULL, [act] nvarchar(3) NULL);
GO
ALTER TABLE [tempimpexp] ADD PRIMARY KEY ([ie],[nr],[source],[source_id],[field_id]);
GO
