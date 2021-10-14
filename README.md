# MeetHut

> University Project work - Projektmunka I-II (2021.)

[![Website](https://img.shields.io/badge/Website-informational?style=for-the-badge)](https://meethut.one)
[![GitHub pull requests](https://img.shields.io/github/issues-pr-raw/sze-plusplusplus/projektmunka?style=for-the-badge)](https://github.com/sze-plusplusplus/projektmunka/pulls)
[![Issue Tracking](https://img.shields.io/badge/YouTrack-Board-blue?style=for-the-badge)](https://plusplusplus.myjetbrains.com/youtrack/agiles/120-2/current)

Online videoconferencing tool for supporting online education designed to be used for online courses, exams.

## Features (currently all todo)

- User, access management, 2FA login
- Timetable: notifications, direct room entering
- Room and video call management (audio/video calls with multiple users, chat, screenshare)
- Exam mode, waitlist

## Documentation

Online: https://meethut.one

Development information: https://meethut.one/development

Deployment information: https://meethut.one/deployment

> Source at [docs/](docs/)

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

## Web documentation

[![Website](https://img.shields.io/badge/Website-informational?style=for-the-badge)](https://meethut.one)

- Livekit instance: https://live.meethut.one
- Application instance: https://app.meethut.one
