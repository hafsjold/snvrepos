SET SQLCE=C:\Programmer\ErikEJ\ExportSqlCe\SqlCeCmd.exe
SET DS="Data Source=C:\Documents and Settings\mha\Dokumenter\Medlem3060\Databaser\SQLCompact\dbData3060.sdf;"

%SQLCE% -d %DS% -e shrink
PAUSE