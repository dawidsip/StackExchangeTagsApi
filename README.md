# Stack Exchange Tags API
This is a ``dotnet webapi`` project to showcase the usage of web 
applications developement paradigms:
* **RESTFULL** API
* ORM for Database abstraction with in memory **Entity Framework**
* Serialisation and deserialisation of **JSON** data
* Paging
* Configuring
* **Swagger** for testing API calls
* **OpenApi**
* Data Persistance
* Dependency Injection
* abstraction
* Logging
* some **SOLID** principles
* Writing tests
* Using **Moq** for mocking objects for tests
* Contenerisation with **Docker** and Docker-Compose 

## Description
The app fetches 1000 records of 'Tags' from the **StackExchange** 
API and saves it in an 'in memory' Database, for later to be served 
with it's own API. The API is described with OpenApi and can be 
tested through **Swagger**.
## Usage & Installation
Easiest way is by launching ``docker compose up`` or simply by running 
the project with ``dotnet run`` and going to the http://localhost:5184/swagger
