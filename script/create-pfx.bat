@echo off

SET ROOT_DIR=%cd%
SET certFile="%ROOT_DIR%\assets\certs\Nets.cer"
SET keyFile="%ROOT_DIR%\assets\certs\Nets.pvk"
SET outputPfxFile="%ROOT_DIR%\assets\certs\Nets.pfx"
SET pvk2pfx="%PROGRAMFILES(x86)%\Windows Kits\10\bin\x64\pvk2pfx"

CALL %pvk2pfx% -f -pvk %keyFile% -spc %certFile% -pfx %outputPfxFile% -pi %npm_package_config_nets_password%
