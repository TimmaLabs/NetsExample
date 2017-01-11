@echo off

SET validFrom="01/11/2017"
SET validTill="01/11/2022"
SET subjectName="Timma"
SET storeName="Root"
SET outputCertFile="Timma.cer"
SET outputKeyFile="Timma.pvk"
SET storeLocation="LocalMachine"
SET makecert="%PROGRAMFILES(x86)%\Windows Kits\10\bin\x64\makecert"

:: For values, see HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography\Defaults\Provider Types via regedit
SET cryptoProvider="Microsoft Enhanced RSA and AES Cryptographic Provider"
SET cryptoProviderType=24

CALL %makecert% -r -pe -a sha256 -n CN=%subjectName% -ss %storeName% -sr %storeLocation% -b %validFrom% -e %validTill% -len 2048 -sp %cryptoProvider% -sy %cryptoProviderType% -sv %outputKeyFile% %outputCertFile%
