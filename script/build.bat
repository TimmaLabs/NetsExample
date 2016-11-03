@echo off

SET conf=%1
SET plat=%2
SET ROOT_DIR=%cd%
SET SCRIPT_DIR=%ROOT_DIR%\script
SET BIN_DIR=bin
SET OUTPUT_DIR=%BIN_DIR%\%plat%\%conf%

SET msbuild="%PROGRAMFILES(x86)%\MSBuild\14.0\Bin\MSBuild"

%msbuild% "app\timma.csproj" /t:rebuild /p:Configuration=%conf%;Platform=%plat%;DefineConstants=PRODUCTION
:: x86 only for the installers
%msbuild% "setup\setup.wixproj" /t:rebuild /p:Configuration=%conf%;Platform=x86;DefineConstants=PRODUCTION
%msbuild% "bundle\bundle.wixproj" /t:rebuild /p:Configuration=%conf%;Platform=x86;DefineConstants=PRODUCTION

:: sign the setup (.msi)
call %SCRIPT_DIR%\sign-msi.bat "%SCRIPT_DIR%\Timma.pfx" %ROOT_DIR%\setup\bin\%conf%\ voittaja
:: sign the bundle (.exe)
call %SCRIPT_DIR%\sign-bundle.bat "%SCRIPT_DIR%\Timma.pfx" %ROOT_DIR%\bundle\bin\%conf%\ voittaja

rd /S /Q %OUTPUT_DIR%

xcopy %ROOT_DIR%\setup\bin\%conf%\* %OUTPUT_DIR% /F /Y /I
xcopy %ROOT_DIR%\bundle\bin\%conf%\* %OUTPUT_DIR% /F /Y /I
copy %SCRIPT_DIR%\Timma.cer %OUTPUT_DIR%
