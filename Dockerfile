FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine

EXPOSE 5000
EXPOSE 5001

ARG USER=dev
ARG DUID=1000
ARG DGID=1000
ARG ISMAC=0

VOLUME [ "/src" ]
WORKDIR /src

RUN if [ ${ISMAC} -eq 0 ]; then \
    addgroup -g ${DGID} -S ${USER}; \
    fi
RUN adduser -S -G ${USER} -u ${DUID} -s /bin/bash ${USER}
USER ${USER}

ENV DOTNET_WATCH_SUPPRESS_LAUNCH_BROWSER 1
ENV DOTNET_USE_POLLING_FILE_WATCHER 1
ENV ASPNETCORE_ENVIRONMENT Development

RUN dotnet dev-certs https --clean && dotnet dev-certs https -t

ENTRYPOINT ["dotnet", "watch", "run", "--project", "MeetHut.Backend/MeetHut.Backend.csproj", "--urls", "http://0.0.0.0:5000;https://0.0.0.0:5001"]
