@echo off

SET certFile="Timma.cer"
SET keyFile="Timma.pvk"
SET outputPfxFile="Timma.pfx"
SET cmd="%PROGRAMFILES(x86)%\Windows Kits\10\bin\x64\pvk2pfx"

CALL "%cmd" -f -pvk %keyFile% -spc %certFile% -pfx %outputPfxFile%
