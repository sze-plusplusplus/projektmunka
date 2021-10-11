FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine

EXPOSE 5000
EXPOSE 5001

ARG USER=dev
ARG DUID=1000
ARG DGID=1000
ARG ISMAC=0

VOLUME [ "/src" ]
WORKDIR /src

# Group with that id can exists (for example mac default user is in group 20)
# M: DUID=501 DGID=20 / 20 is dialout
RUN addgroup -g ${DGID} -S ${USER} || echo Not adding group
# Because the group can be pre existing, but we want to use that id, we get the name of it and use it for the user creation
RUN adduser -S -G $(getent group ${DGID} | cut -d ":" -f1) -u ${DUID} -s /bin/bash ${USER}
USER ${USER}

ENV DOTNET_WATCH_SUPPRESS_LAUNCH_BROWSER 1
ENV DOTNET_USE_POLLING_FILE_WATCHER 1
ENV ASPNETCORE_ENVIRONMENT Development
ENV DOTNET_CLI_TELEMETRY_OPTOUT 1

RUN dotnet dev-certs https --clean && dotnet dev-certs https -t

ENTRYPOINT ["dotnet", "watch", "run", "--project", "MeetHut.Backend/MeetHut.Backend.csproj", "--urls", "http://0.0.0.0:5000;https://0.0.0.0:5001"]
