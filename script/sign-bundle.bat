@echo off

SET pfxPath=%1
SET targetDir=%2
SET password=%3
SET enginePath="%targetDir%engine.exe"
SET signtool="%PROGRAMFILES(x86)%\Windows Kits\10\bin\x64\signtool"
SET insignia="%PROGRAMFILES(x86)%\WiX Toolset v3.10\bin\insignia"

for /f %%i in ('dir /b %targetDir%*.bundle.exe') do SET bundlePath=%targetDir%%%i
CALL %insignia% -v -nologo -ib %bundlePath% -o %enginePath%
CALL %signtool% sign /v /f %pfxPath% /p %password% %enginePath%
CALL %insignia% -v -nologo -ab %enginePath% %bundlePath% -o %bundlePath%
CALL %signtool% sign /v /f %pfxPath% /d Timma /du https://timma.fi/company /p %password% %bundlePath%
CALL del %enginePath%