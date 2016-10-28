@echo off

SET pfxPath=%1
SET sourceDir=%2
SET password=%3
SET signtool="%PROGRAMFILES(x86)%\Windows Kits\10\bin\x64\signtool"
SET insignia="%PROGRAMFILES(x86)%\WiX Toolset v3.10\bin\insignia"

for /f %%i in ('dir /b %sourceDir%*.cab') do SET cabPath=%sourceDir%%%i
for /f %%i in ('dir /b %sourceDir%*.msi') do SET msiPath=%sourceDir%%%i

CALL %signtool% sign /v /f %pfxPath% /p %password% %cabPath%
CALL %insignia% -v -nologo -im %msiPath%
CALL %signtool% sign /v /f %pfxPath% /d Timma /du https://timma.fi/company /p %password% %msiPath%
