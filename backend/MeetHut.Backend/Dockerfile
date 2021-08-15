FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MeetHut.Backend/MeetHut.Backend.csproj", "MeetHut.Backend/"]
RUN dotnet restore "MeetHut.Backend/MeetHut.Backend.csproj"
COPY . .
WORKDIR "/src/MeetHut.Backend"
RUN dotnet build "MeetHut.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeetHut.Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeetHut.Backend.dll"]
