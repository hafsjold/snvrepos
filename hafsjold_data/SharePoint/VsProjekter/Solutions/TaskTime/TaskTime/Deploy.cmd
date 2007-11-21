@echo Deploying TaskTime
@set PATH=C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN;%PATH%
@set URL="http://localhost"

stsadm -o deactivatefeature -name TaskTimeCustomerList -url %URL%
stsadm -o deactivatefeature -name TaskTimeProjectTimeList -url %URL%
stsadm -o deactivatefeature -name TaskTimeTypes -url %URL%
stsadm -o deactivatefeature -name TaskTimeFields -url %URL%

stsadm -o retractsolution -name TaskTime.wsp -immediate -allcontenturls
stsadm -o execadmsvcjobs
stsadm -o deletesolution -name TaskTime.wsp -override
stsadm -o execadmsvcjobs

stsadm -o addsolution -filename bin\TaskTime.wsp
stsadm -o execadmsvcjobs

stsadm -o deploysolution -name TaskTime.wsp -immediate -allcontenturls -allowGacDeployment -allowCasPolicies
stsadm -o execadmsvcjobs

stsadm -o activatefeature -name TaskTimeFields -url %URL%
stsadm -o activatefeature -name TaskTimeTypes -url %URL%
stsadm -o activatefeature -name TaskTimeProjectTimeList -url %URL%
stsadm -o activatefeature -name TaskTimeCustomerList -url %URL%

