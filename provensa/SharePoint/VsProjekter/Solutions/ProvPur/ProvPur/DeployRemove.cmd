@cls
@echo Deploying ProvPur
@set PATH=C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN;%PATH%
@set URL="http://localhost"

rem stsadm -o deactivatefeature -name PersonList -url %URL%
rem stsadm -o deactivatefeature -name ProvPurTypes -url %URL%
rem stsadm -o deactivatefeature -name ProvPurFields -url %URL%

rem stsadm -o retractsolution -name ProvPur.wsp -immediate -allcontenturls
rem stsadm -o execadmsvcjobs

stsadm -o deletesolution -name ProvPur.wsp -override
stsadm -o execadmsvcjobs
