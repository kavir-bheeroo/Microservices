# Microservices
What's better than coding, to learn microservices?

## Plan of Work
- Create a Web API that exposes a resource / CRUD-type
- Connect to a data store - Abstract DB connection so relational, NoSQL or cache can be used.
- Dockerise API and services
- Implement an Identity Server and secure Web API - maybe in an API Gateway
- Add a second microservice that listens to event from initial Web API
- Implement a Service Bus - perhaps use EventSourcing
- Add health checks
- Move the entire stack to an orchestrator - Kubernetes
- Integrate with the ELK stack

## Microservices to build
- The general idea with this example is to build a Bus Fleet management system in Microservices-style. The following services will be built:
1. Resources - to perform CRUD for buses, drivers and conductors
2. Roster - generate a tentative schedule for buses and workers
3. Payroll - maintain attendance and calculate the pay of employees
4. Revenue - DDD approach
    - Add trip details and adjust daily revenue
    - Two aggregates - Trip and DailyRevenue.
    - Trip mush raise a domain event to update revenue and integration event to update payroll.
    - Update revenue when a trip is updated.
5. Identity Server - All configuration are stored in SQL Server and managed via EF Core.

## Technologies used
- Docker
- Event Bus - RabbitMQ
- Redis
- Mediator Pattern with MediatR
- IdentityServer4
- EntityFramework Core