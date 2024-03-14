# Cascade
## Overview
A robust and flexible music player designed to provide an immersive listening experience for users.
## Build Status
[![.NET Core Desktop](https://github.com/Jordan-Dimitrov/Cascade/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/Jordan-Dimitrov/Cascade/actions/workflows/dotnet-desktop.yml)
## Architecture
Cascade is a Domain-Driven Modular Monolith. These foundational concepts ensure a robust, scalable, and maintainable music playback solution. 
Below you can find an accompanying UML diagram illustrating its architecture and components:

![312240646-afcf2861-23a7-4f75-ab55-7eb96512d7bc](https://github.com/Jordan-Dimitrov/Cascade/assets/91904012/5f21d4a4-3f6f-419d-9af9-6e36ed73377d)

## Patterns
It uses the following patterns:
- [Mediator](https://refactoring.guru/design-patterns/mediator)
- [CQRS](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [Outbox](https://microservices.io/patterns/data/transactional-outbox.html)
- [Decorator](https://refactoring.guru/design-patterns/decorator)
- [Hateoas](https://medium.com/spring-framework/hateoas-design-principle-giving-power-to-your-application-backend-cb1eb5ef2976)
- [Read-Through](https://www.enjoyalgorithms.com/blog/read-through-caching-strategy)
- [Unit of Work](https://medium.com/@edin.sahbaz/implementing-the-unit-of-work-pattern-in-clean-architecture-with-net-core-53efb7f9d4d)
## Technologies
Below is a comprehensive list of technologies, frameworks, and libraries utilized in the implementation:
- [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (Platform)
- [Entity Framework Core 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0](https://learn.microsoft.com/en-us/ef/)) (ORM)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Database)
- [Polly](https://github.com/App-vNext/Polly) (Resilience and transient-fault-handling library)
- [Mediatr](https://github.com/jbogard/MediatR) (Mediator implementation)
- [XUnit](https://xunit.net/) (Testing framework)
- [FakeItEasy](https://fakeiteasy.github.io/) (Mocking framework)
- [NetArchTest](https://github.com/BenMorris/NetArchTest.git) (Architecture Unit Tests library)
- [Newtonsoft.Json](https://www.newtonsoft.com/json) (JSON framework)
- [Redis](https://redis.io/) (In-memory data store)
- [RabbitMQ](https://www.rabbitmq.com/) (Message broker)
- [Mass transit](https://masstransit.io/) (Distributed application framework)
- [Quartz](https://www.quartz-scheduler.net/) (Job scheduling system)
- [Serilog](https://serilog.net/) (Logging Library)
- [Scrutor](https://github.com/khellang/Scrutor) (Decorator library)
- [FFmpeg](https://ffmpeg.org/) (Multimedia framework)
- [ATL](https://github.com/Zeugma440/atldotnet.git) (Audio Library)

## Getting Started
### Prerequisites
Before you begin, ensure you have the following:

1. [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

2. [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

3. [FFmpeg](https://www.ffmpeg.org/download.html)

4. [Redis](https://redis.io/)

5. [RabbitMQ](https://www.rabbitmq.com/)

6. FTP Server
### Installation
1. Fill out the appsettings.Development.json in the CascadeAPI Project
2. Run the project:
```bash
dotnet run --environment Development
```
## Contribution
This project is still under analysis and development. I would appreciate your contribution to it. Please let me know by creating an Issue or Pull Request.
