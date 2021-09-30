@echo off

SET DOTNET_CLI_TELEMETRY_OPTOUT=1

:loop 
if "%1"=="" goto :done
if "%1"=="restore" (call dotnet restore)
if "%1"=="watch" (call dotnet watch run --project MeetHut.Backend/MeetHut.Backend.csproj --urls http://0.0.0.0:5000;https://0.0.0.0:5001)
if "%1"=="publish" (call fe.bat install & cd Meethut.Backend & call dotnet publish -c Release)
shift
goto :loop

:done
echo Done.