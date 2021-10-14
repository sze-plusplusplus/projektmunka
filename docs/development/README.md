# Development

## Platforms

Development/Tested operationg systems: Ubuntu Focal, Windows 10/11, MacOS Big Sur

Docker is used on Linux and Mac hosts, on Windows no containerization used

## Linux/MacOS

### Running locally in development mode w/ docker-compose (watch)

- Create / edit the required config files, the default docker-compose file
  - Backend: `./MeetHut.Backend/appsettings.json` (or according to env: `appsettings.Development.json`)
  - Livekit: `./.livekit_config.yml`
  - default db credentials: `meethut:Abc123456`
  - with the given docker-compose.yml all service ports are exposed to 127.0.0.1
    - (app 5000[http], 5001[https], adminer 81, mysql 3306, livekit 7880[web],7881,7882)
- `make up` - Creates a container with dotnet watch ... command, a frontend container, a mariadb database container and adminer for database access
  - App access: http://localhost:5000/ (ASP app proxies the angular app)
  - Adminer: http://localhost:81/

### Build release docker image

- `make publish` will build a new multiarch image (linux/(amd|arm)64) and push to ghcr.io (permission required to org)

### Running release version

- Use strong JWT / access keys, passwords!
- `make image` starts the released image

### Other commands

- `make build` - Build FE and BE - `make build-fe; make build-be`
- `make test` - Run tests for BE and FE
- `make add-migration name=Init` - Add backend EF migration
- `make gettoken room=a user=ben` - Create a room joining token for the livekit server (for test purposes)

## Windows

Starting, developing without docker, on Windows.

### Helper commands

- FE
  - `.\fe.bat install` - install deps with frozen lock file
  - `.\fe.bat start` - start in development mode
  - `.\fe.bat install build` - install, then build in prod mode
- BE
  - `.\be.bat restore` - dotnet restore
  - `.\be.bat watch` - runs the Meethut.Backend project in watch mode
  - `.\be.bat migration Init` - add backend EF migration
  - `.\be.bat publish` - installs npm deps, builds NETCore and Angular app
