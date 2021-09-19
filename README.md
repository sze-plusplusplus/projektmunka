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

- `docker-compose up` - Creates a container with `dotnet watch ...` command, a frontend container, a mariadb database container and adminer for database access
- App access: http://localhost:5000/ (ASP app proxies the angular app)
- Adminer: http://localhost:81/
- with the given `docker-compose.yml` all service ports are exposed to `127.0.0.1` (app 5000[http], 5001[https], adminer 81, mysql 3306)
- default db credentials: meethut:Abc123456

### Build release image

- `docker-compose -f docker-compose.publish.yml build` will build a new image (release mode, image name: "meethut-backend")

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

| #   | Nev                     | Main responsibility | Github                                             |
| --- | ----------------------- | ------------------- | -------------------------------------------------- |
| 1   | Karcag Tamás            | Web backend         | [@karcagtamas](https://github.com/karcagtamas)     |
| 2   | Baranyai Bence Bendegúz | Media backend       | [@bencebaranyai](https://github.com/bencebaranyai) |
| 3   | Balogh Máté             | Web frontend        | [@Cerberuuus](https://github.com/Cerberuuus)       |
| 4   | Tóth Róbert             | Media frontend      | [@tothrobi](https://github.com/tothrobi)           |

### Details of the university exercise (in Hungarian): [README.HU.md](README.HU.md)
