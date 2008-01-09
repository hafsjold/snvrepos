@cls
@echo Deploying ProvPur
@set PATH=C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN;%PATH%
rem @set URL="http://localhost"

stsadm -o retractsolution -name Kurv.wsp -local -allcontenturls

stsadm -o deletesolution -name Kurv.wsp -override
stsadm -o execadmsvcjobs

stsadm -o addsolution -filename bin\Debug\Kurv.wsp
stsadm -o execadmsvcjobs

stsadm -o deploysolution -name Kurv.wsp -local -allcontenturls -allowGacDeployment -allowCasPolicies

IISRESET