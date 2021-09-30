# MeetHut

> University Project work - Projektmunka I-II (2021.)

[![GitHub pull requests](https://img.shields.io/github/issues-pr-raw/sze-plusplusplus/projektmunka?style=for-the-badge)](https://github.com/sze-plusplusplus/projektmunka/pulls)
[![Issue Tracking](https://img.shields.io/badge/YouTrack-Board-blue?style=for-the-badge)](https://plusplusplus.myjetbrains.com/youtrack/agiles/120-2/current)

Online videoconferencing tool for supporting online education designed to be used for online courses, exams.

## Features (currently all todo)

- User, access management, 2FA login
- Timetable: notifications, direct room entering
- Room and video call management (audio/video calls with multiple users, chat, screenshare)
- Exam mode, waitlist

## Development

### Running locally in development mode w/ docker-compose (watch)

- Create / edit the required config files, the default docker-compose file
  - Backend: `./MeetHut.Backend/appsettings.json`
  - Livekit: `./.livekit_config.yml`
  - default db credentials: meethut:Abc123456
  - with the given `docker-compose.yml` all service ports are exposed to `127.0.0.1` (app 5000[http], 5001[https], adminer 81, mysql 3306, livekit 7880[web],7881,7882)
- `make up` - Creates a container with `dotnet watch ...` command, a frontend container, a mariadb database container and adminer for database access
- App access: http://localhost:5000/ (ASP app proxies the angular app)
- Adminer: http://localhost:81/

### Build release image

- `make publish` will build a new image (release mode, image name: "meethut", exported file: "./publish/meethut.tar")

### Running release version

- Use strong JWT / access keys, passwords!
- `make image` starts the released image

### Other commands

- `make build` - Build FE and BE - `make build-fe; make build-be`
- `make test` - Run tests for BE and FE
- `make gettoken room=a user=ben` - Create a room joining token for the livekit server (for test purposes)

### Windows commands

> starting, developing without docker, on Windows

- FE
    - `.\fe.bat install` - install deps with frozen lock file
    - `.\fe.bat start` - start in development mode
    - `.\fe.bat install build` - install, then build in prod mode
- BE
    - `.\be.bat restore` - dotnet restore
    - `.\be.bat watch` - runs the Meethut.Backend project in watch mode
    - `.\be.bat publish` - installs npm deps, builds NETCore and Angular app

## Project structure

- .
  - Docker(-compose) related files, solution
- ./MeetHut.Backend
  - AspNetCore main application
  - ./ClientApp
    - Angular frontend app
- ./MeetHut.CommonTools
  - Common functions
- ./MeetHut.DataAccess
  - Database layer (EntityFramework)
- ./MeetHut.Services
  - Services, Models

## Team:

| #   | Name                    | Main responsibility | Github                                             |
| --- | ----------------------- | ------------------- | -------------------------------------------------- |
| 1   | Karcag Tamás            | Web backend         | [@karcagtamas](https://github.com/karcagtamas)     |
| 2   | Baranyai Bence Bendegúz | Media service       | [@bencebaranyai](https://github.com/bencebaranyai) |
| 3   | Balogh Máté             | Web frontend        | [@Cerberuuus](https://github.com/Cerberuuus)       |
| 4   | Tóth Róbert             | Media frontend      | [@tothrobi](https://github.com/tothrobi)           |

### Details of the university exercise (in Hungarian): [README.HU.md](README.HU.md)

## Main Tasks:

> https://plusplusplus.myjetbrains.com/youtrack/agiles/120-2/current

| #                                                                  | Task, Functionality      | Milestone | Depends on  | Assignee            |
| ------------------------------------------------------------------ | ------------------------ | --------- | ----------- | ------------------- |
| [PM-1](https://plusplusplus.myjetbrains.com/youtrack/issue/PM-1)   | Project init             | M0        | -           | Team                |
| [PM-3](https://plusplusplus.myjetbrains.com/youtrack/issue/PM-3)   | Dev tooling              | M1        | PM-1        | B. Bence, K. Tamás  |
| [PM-4](https://plusplusplus.myjetbrains.com/youtrack/issue/PM-4)   | Livekit SDK and int.     | M1        | PM-1        | B. Bence            |
| [PM-2](https://plusplusplus.myjetbrains.com/youtrack/issue/PM-2)   | GUI planning (wireframe) | M1        | -           | T. Róbert, B. Máté  |
| [PM-6](https://plusplusplus.myjetbrains.com/youtrack/issue/PM-6)   | Room management          | M2        | PM-2, PM-3  | B. Bence, T. Róbert |
| [PM-8](https://plusplusplus.myjetbrains.com/youtrack/issue/PM-8)   | Login / Register / Audit | M2        | PM-2, PM-3  | K. Tamás, T. Róbert |
| [PM-7](https://plusplusplus.myjetbrains.com/youtrack/issue/PM-7)   | User management          | M2        | PM-1, PM-8  | K. Tamás, B. Máté   |
| [PM-9](https://plusplusplus.myjetbrains.com/youtrack/issue/PM-9)   | Conference Base GUI      | M2        | PM-1, PM-4  | K. Tamás, B. Bence  |
| [PM-10](https://plusplusplus.myjetbrains.com/youtrack/issue/PM-10) | Timetable                | M3        | PM-8, PM-7  | K. Tamás, T. Róbert |
| [PM-11](https://plusplusplus.myjetbrains.com/youtrack/issue/PM-11) | Exam mode                | M3        | PM-9        | K. Tamás, B. Máté   |
| [PM-12](https://plusplusplus.myjetbrains.com/youtrack/issue/PM-12) | Conference UI            | M3        | PM-9        | T. Róbert, B. Máté  |
| [PM-13](https://plusplusplus.myjetbrains.com/youtrack/issue/PM-13) | Conference Chat          | M3        | PM-9, PM-12 | T. Róbert, B. Bence |
| [PM-14](https://plusplusplus.myjetbrains.com/youtrack/issue/PM-14) | Conference Settings      | M4        | PM-12       | B. Máté, B. Bence   |
