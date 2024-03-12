# Cascade
## Overview
A robust and flexible music player designed to provide an immersive listening experience for users.
## Build Status
[![.NET Core Desktop](https://github.com/Jordan-Dimitrov/Cascade/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/Jordan-Dimitrov/Cascade/actions/workflows/dotnet-desktop.yml)
## Architecture
Cascade is a Domain-Driven Modular Monolith. These foundational concepts ensure a robust, scalable, and maintainable music playback solution. It uses the following patterns:
- [Mediator](https://refactoring.guru/design-patterns/mediator)
- [CQRS](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [Outbox](https://microservices.io/patterns/data/transactional-outbox.html)
- [Decorator](https://refactoring.guru/design-patterns/decorator)
- [Hateoas](https://medium.com/spring-framework/hateoas-design-principle-giving-power-to-your-application-backend-cb1eb5ef2976)
- [Read-Through](https://www.enjoyalgorithms.com/blog/read-through-caching-strategy)
## Technologies
Below is a comprehensive list of technologies, frameworks, and libraries utilized in the implementation, along with their respective purposes:
- [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (platform)
- [Entity Framework Core 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0](https://learn.microsoft.com/en-us/ef/)) (ORM)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (database)
- [Polly](https://github.com/App-vNext/Polly) (Resilience and transient-fault-handling library)
- [Mediatr](https://github.com/jbogard/MediatR) (Mediator implementation)
- [XUnit](https://xunit.net/) (Testing framework)
- [FakeItEasy](https://fakeiteasy.github.io/) (Mocking framework)
- [NetArchTest](https://github.com/BenMorris/NetArchTest.git) (Architecture Unit Tests library)
- [Newtonsoft.Json](https://www.newtonsoft.com/json) (JSON framework)
- [Redis](https://redis.io/) (In-memory data store)
- [Rabbitmq](https://www.rabbitmq.com/) (Message broker)
- [Mass transit](https://masstransit.io/) (Distributed application framework)
- [Quartz](https://www.quartz-scheduler.net/) (Job scheduling system)
- [Serilog](https://serilog.net/) (Logging Library)
- [Scrutor](https://github.com/khellang/Scrutor) (Decorator library)
- [FFmpeg](https://ffmpeg.org/) (Multimedia framework)
- [ATL](https://github.com/Zeugma440/atldotnet.git) (Audio Library)

