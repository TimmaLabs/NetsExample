@echo off

SET ROOT_DIR=%cd%
SET SCRIPT_DIR=%ROOT_DIR%\script
SET build=%SCRIPT_DIR%\build\build.bat

call %build% Release x64
call %build% Release x86
