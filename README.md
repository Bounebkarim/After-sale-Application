# MiniProjet
##Overview

This repository houses a microservice project that follows the principles of Clean Architecture. Clean Architecture, as advocated by Robert C. Martin, emphasizes separation of concerns and maintainability by organizing code into distinct layers, each with a well-defined responsibility.

##Project Structure


- Core: Defines the core business logic and entities without dependencies on external frameworks. This layer encapsulates the essential business rules of the microservice.

- API: Represents the interfaces layer, serving as adapters for external communication. This layer includes controllers, serializers, and other components required for handling API requests and responses.

- Infrastructure: Contains the external frameworks, tools, and drivers necessary for the microservice. This includes database implementations, third-party libraries, and other infrastructure-related code.

##Lessons Learned

1. Maintainability
The Clean Architecture structure has greatly enhanced maintainability. Changes in external tools or frameworks primarily impact the Infrastructure layer, leaving the core business logic in the Core layer stable and adaptable.

2. Testability
The separation of concerns enables easier testing. Core logic can be unit tested without requiring external dependencies, leading to more robust and reliable tests.

3. Scalability
The modular design supports scalability by allowing each layer to be scaled independently. This flexibility is crucial for accommodating increased demand on specific components.

4. Flexibility
Clean Architecture provides flexibility in choosing and swapping out external dependencies. The API and Infrastructure layers act as adaptors, allowing seamless integration of different frameworks and tools.

5. Team Collaboration
Clean Architecture facilitates collaboration among team members. With clear boundaries between layers, developers can work on different components simultaneously, reducing conflicts and streamlining development.

##Getting Started

To run the .NET microservice locally, follow these steps:

1. Clone the repository: git clone https://github.com/Bounebkarim/After-sale-Application.git
2. Navigate to the project directory: cd After-sale-Application
3. Build the solution: dotnet build
3. Run the microservice: dotnet run
Ensure you have the necessary .NET SDK installed for building and running the microservice.
4. run rabbitmq docker image

```bash
  docker run -d -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```
5. run mongodb docker image

```bash
  docker run -d -p 27017:27017 mongo:latest
```
6. run Consul docker image

```bash
  docker run -d -p 8500:8500 consul:1.15.4
```
## Demo

### Consul registration
![image](https://github.com/Bounebkarim/After-sale-Application/assets/72360478/fab815f2-6b89-4920-9a52-511093fa0573)
### Application structure
![image](https://github.com/Bounebkarim/After-sale-Application/assets/72360478/d0e5652e-8b22-476e-b7cf-848f36cf951c)

## Roadmap

- 

- Add more integrations



## Authors

- [@EmnaBenZina](https://github.com/benzinaemna)
- [@KarimBouneb](https://github.com/Bounebkarim)
