@echo off

SET pfxPath=%1
SET targetDir=%2
SET password=%3
SET signtool="%PROGRAMFILES(x86)%\Windows Kits\10\bin\x64\signtool"
SET insignia="%PROGRAMFILES(x86)%\WiX Toolset v3.10\bin\insignia"

for /f %%i in ('dir /b %targetDir%*.cab') do SET cabPath=%targetDir%%%i
for /f %%i in ('dir /b %targetDir%*.msi') do SET msiPath=%targetDir%%%i

CALL %signtool% sign /v /f %pfxPath% /p %password% %cabPath%
CALL %insignia% -v -nologo -im %msiPath%
CALL %signtool% sign /v /f %pfxPath% /d NetsExample /du https://nets.eu /p %password% %msiPath%
