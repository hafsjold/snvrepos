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
