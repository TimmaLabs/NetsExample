@echo off

SET validFrom="10/01/2016"
SET validTill="10/01/2017"
SET subjectName="Timma Oy Ab"
SET storeName="Timma"
SET outputCertFile="Timma.cer"
SET outputKeyFile="Timma.pvk"
SET storeLocation="CurrentUser"
SET cmd="%PROGRAMFILES(x86)%\Windows Kits\10\bin\x64\makecert"

:: For values, see HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography\Defaults\Provider Types via regedit
SET cryptoProvider="Microsoft Enhanced RSA and AES Cryptographic Provider"
SET cryptoProviderType=24

CALL "%cmd" -r -pe -a sha256 -n CN=%subjectName% -ss %storeName% -sr %storeLocation% -b %validFrom% -e %validTill% -len 2048 -sp %cryptoProvider% -sy %cryptoProviderType% -sv %outputKeyFile% %outputCertFile%
