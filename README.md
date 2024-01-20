# MiniProjet
## Installation

run rabbitmq docker image

```bash
  docker run -d -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```
run mongodb docker image

```bash
  docker run -d -p 27017:27017 mongo:latest
```
run Consul docker image

```bash
  docker run -d -p 8500:8500 consul:1.15.4
```
## Lessons Learned

We used simple microservice logic while emplimenting Clean architechure to our best ability.
We tryed pagination, specification pattern,CQRS,Repository,Mediatr and service registration with ocelot + consul.

## Demo
### Consul registration
![image](https://github.com/Bounebkarim/After-sale-Application/assets/72360478/fab815f2-6b89-4920-9a52-511093fa0573)
### Application structure
![image](https://github.com/Bounebkarim/After-sale-Application/assets/72360478/d0e5652e-8b22-476e-b7cf-848f36cf951c)



## Authors

- [@EmnaBenZina](https://github.com/benzinaemna)
- [@KarimBouneb](https://github.com/Bounebkarim)
