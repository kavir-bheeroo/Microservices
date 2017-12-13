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