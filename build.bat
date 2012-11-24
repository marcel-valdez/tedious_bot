@ECHO off
SET compile=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe build.proj /nologo

ECHO ********************************
ECHO Building Tools
%compile%
IF %errorlevel% neq 0 EXIT /b %errorlevel%
