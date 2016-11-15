@echo off

setlocal enabledelayedexpansion

SET mode=%1
SET arch=%2
SET local_build=%3

SET ROOT_DIR=%cd%
SET SCRIPT_DIR=%ROOT_DIR%\script
SET BIN_DIR=bin
SET OUTPUT_DIR=%BIN_DIR%\%mode%\%arch%
SET CONSTANTS=PRODUCTION

SET msbuild="%PROGRAMFILES(x86)%\MSBuild\14.0\Bin\MSBuild"

if "%mode%" == "Debug" (
	SET CONSTANTS=%CONSTANTS%;DEBUG
)

if DEFINED local_build  (
	SET CONSTANTS=%CONSTANTS%;LOCAL
)

%msbuild% "app\timma.csproj" /t:rebuild /p:Configuration=%mode%;Platform=%arch%;DefineConstants="!CONSTANTS!"
:: x86 only for the installers
%msbuild% "setup\setup.wixproj" /t:rebuild /p:Configuration=%mode%;Platform=x86;DefineConstants="!CONSTANTS!"
%msbuild% "bundle\bundle.wixproj" /t:rebuild /p:Configuration=%mode%;Platform=x86;DefineConstants="!CONSTANTS!"

:: sign the setup (.msi)
call %SCRIPT_DIR%\sign-msi.bat "%SCRIPT_DIR%\Timma.pfx" %ROOT_DIR%\setup\bin\%mode%\ voittaja
:: sign the bundle (.exe)
call %SCRIPT_DIR%\sign-bundle.bat "%SCRIPT_DIR%\Timma.pfx" %ROOT_DIR%\bundle\bin\%mode%\ voittaja

rd /S /Q %OUTPUT_DIR%

xcopy %ROOT_DIR%\setup\bin\%mode%\* %OUTPUT_DIR% /F /Y /I
xcopy %ROOT_DIR%\bundle\bin\%mode%\* %OUTPUT_DIR% /F /Y /I
copy %SCRIPT_DIR%\Timma.cer %OUTPUT_DIR%
