--Delete outdated tblaftalelin records
CREATE TABLE [tempaftalelin] (
  [id] int NULL
, [frapbsid] int NULL
, [pbstranskode] nvarchar(4) NULL
, [Nr] int NULL
, [debitorkonto] nvarchar(15) NULL
, [debgrpnr] nvarchar(5) NULL
, [aftalenr] int NULL
, [aftalestartdato] datetime NULL
, [aftaleslutdato] datetime NULL
, [pbssektionnr] nvarchar(5) NOT NULL
);
GO
--find and indsert the latest  record with pbstranskode = '0230'
insert into tempaftalelin (id, frapbsid, pbstranskode, Nr, debitorkonto, debgrpnr, aftalenr, aftalestartdato, aftaleslutdato, pbssektionnr)
       select top(1) c.id, c.frapbsid, c.pbstranskode, c.Nr, c.debitorkonto, c.debgrpnr, c.aftalenr, c.aftalestartdato, c.aftaleslutdato, c.pbssektionnr
       from tblaftalelin c join tblfrapbs d on c.frapbsid = d.id where c.pbstranskode = '0230' order by d.leverancedannelsesdato desc;
GO
delete tblaftalelin where id in (select a.id from tblaftalelin a  join tblfrapbs b on a.frapbsid = b.id  ,tempaftalelin c join tblfrapbs d on c.frapbsid = d.id 
       where b.leverancedannelsesdato < d.leverancedannelsesdato);
GO
DROP TABLE tempaftalelin;
GO