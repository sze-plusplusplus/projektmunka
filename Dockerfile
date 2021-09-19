FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine

EXPOSE 5000
EXPOSE 5001

VOLUME [ "/src" ]
WORKDIR /src

ENV DOTNET_WATCH_SUPPRESS_LAUNCH_BROWSER 1
ENV DOTNET_USE_POLLING_FILE_WATCHER 1
ENV ASPNETCORE_ENVIRONMENT Development

RUN dotnet dev-certs https --clean && dotnet dev-certs https -t

ENTRYPOINT ["dotnet", "watch", "run", "--project", "MeetHut.Backend/MeetHut.Backend.csproj", "--urls", "http://0.0.0.0:5000;https://0.0.0.0:5001"]
