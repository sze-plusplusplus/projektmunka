FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

RUN apt-get update
RUN apt-get install nodejs npm -y
RUN npm i -g npm

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

RUN apt-get update
RUN apt-get install nodejs npm -y
RUN npm i -g npm

WORKDIR /src
COPY *.sln .
COPY MeetHut.Backend/*.csproj ./MeetHut.Backend/
COPY MeetHut.CommonTools/*.csproj ./MeetHut.CommonTools/
COPY MeetHut.DataAccess/*.csproj ./MeetHut.DataAccess/
COPY MeetHut.Services/*.csproj ./MeetHut.Services/

RUN dotnet restore

COPY MeetHut.Backend/. ./MeetHut.Backend/
COPY MeetHut.CommonTools/. ./MeetHut.CommonTools/
COPY MeetHut.DataAccess/. ./MeetHut.DataAccess/
COPY MeetHut.Services/. ./MeetHut.Services/

WORKDIR "/src/MeetHut.Backend"
RUN dotnet build "MeetHut.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeetHut.Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeetHut.Backend.dll"]
