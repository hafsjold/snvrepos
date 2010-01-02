SET SQLCE=C:\Programmer\ErikEJ\ExportSqlCe\SqlCeCmd.exe
SET DS="Data Source=C:\Documents and Settings\mha\Dokumenter\Medlem3060\Databaser\SQLCompact\dbData3060.sdf;"
SET SC="C:\Documents and Settings\mha\Dokumenter\Medlem3060\Databaser\SQLCompact\Scripts\PBS.sqlce"
SET LOG="C:\Documents and Settings\mha\Dokumenter\Medlem3060\Databaser\SQLCompact\Scripts\PBS.log"
%SQLCE% -d %DS% -i %SC% -o %LOG%
PAUSE
