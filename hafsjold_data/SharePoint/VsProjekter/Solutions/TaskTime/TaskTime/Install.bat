@SET TEMPLATEDIR="c:\program files\common files\microsoft shared\web server extensions\12\Template"
@SET STSADM="c:\program files\common files\microsoft shared\web server extensions\12\bin\stsadm"
@SET GACUTIL="c:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\Bin\gacutil.exe"

Echo Installing HelloWorld.dll in GAC
%GACUTIL% -if bin\debug\TaskTime.dll

Echo Copying files to TEMPLATE directory
xcopy /e /y TEMPLATE\* %TEMPLATEDIR%

Echo Installing feature
%STSADM% -o installfeature -filename  TaskTimeFields\feature.xml -force
%STSADM% -o installfeature -filename  TaskTimeTypes\feature.xml -force
%STSADM% -o installfeature -filename  TaskTimeProjectTimeList\feature.xml -force
%STSADM% -o installfeature -filename  TaskTimeCustomerList\feature.xml -force

IISRESET
REM cscript c:\windows\system32\iisapp.vbs /a "SharePointDefaultAppPool" /r
