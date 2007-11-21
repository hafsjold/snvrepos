@echo Deploying LitwareAjaxWebParts

@set PATH=C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN;%PATH%

stsadm -o deactivatefeature -name LitwareAjaxWebParts -url http://localhost

stsadm -o retractsolution -name LitwareAjaxWebParts.wsp -immediate -allcontenturls
stsadm -o execadmsvcjobs
stsadm -o deletesolution -name LitwareAjaxWebParts.wsp -override
stsadm -o execadmsvcjobs

stsadm -o addsolution -filename bin\LitwareAjaxWebParts.wsp
stsadm -o execadmsvcjobs

stsadm -o deploysolution -name LitwareAjaxWebParts.wsp -immediate -allcontenturls -allowGacDeployment -allowCasPolicies
stsadm -o execadmsvcjobs

stsadm -o activatefeature -name LitwareAjaxWebParts -url http://localhost

