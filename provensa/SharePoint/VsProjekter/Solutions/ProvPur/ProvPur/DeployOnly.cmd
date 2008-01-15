@cls
@echo Deploying ProvPur
@set PATH=C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN;%PATH%
rem @set URL="http://localhost"
stsadm -o addsolution -filename bin\Debug\ProvPur.wsp
stsadm -o execadmsvcjobs

rem stsadm -o deploysolution -name ProvPur.wsp -immediate -allcontenturls -allowGacDeployment -allowCasPolicies
rem stsadm -o execadmsvcjobs

rem stsadm -o activatefeature -name ProvPurFields -url %URL%
rem stsadm -o activatefeature -name ProvPurTypes -url %URL%
rem stsadm -o activatefeature -name PersonList -url %URL%
rem stsadm -o activatefeature -name ProvPurSitePages -url %URL%

pause
