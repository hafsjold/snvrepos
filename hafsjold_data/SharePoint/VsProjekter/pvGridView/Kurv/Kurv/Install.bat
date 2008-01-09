
@SET TEMPLATEDIR="c:\program files\common files\microsoft shared\web server extensions\12\Template"
@SET STSADM="c:\program files\common files\microsoft shared\web server extensions\12\bin\stsadm"
@SET GACUTIL="c:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\Bin\gacutil.exe"

Echo Installing Kurv.dll in GAC
%GACUTIL% -uf spListDB
%GACUTIL% -uf Kurv
rem %GACUTIL% -if ..\spListDB\bin\debug\spListDB.dll
rem %GACUTIL% -if bin\debug\Kurv.dll


Echo Copying files
xcopy /e /y TEMPLATE\* %TEMPLATEDIR%

IISRESET

pause

/uf assemblyName
