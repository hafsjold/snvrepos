@echo Deploying ProvPur
@set PATH=C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN;%PATH%
@set URL="http://localhost"

stsadm -o deactivatefeature -name ProvPurProductList -url %URL%
stsadm -o deactivatefeature -name ProvPurPurOrderList -url %URL%
stsadm -o deactivatefeature -name ProvPurTypes -url %URL%
stsadm -o deactivatefeature -name ProvPurFields -url %URL%

stsadm -o retractsolution -name ProvPur.wsp -immediate -allcontenturls
stsadm -o execadmsvcjobs
stsadm -o deletesolution -name ProvPur.wsp -override
stsadm -o execadmsvcjobs
