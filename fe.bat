@echo off

cd Meethut.Backend\ClientApp\

:loop 
if "%1"=="" goto :done
if "%1"=="install" (call yarn install --frozen-lockfile) else (call yarn %1)
shift
goto :loop

:done
cd ..\..