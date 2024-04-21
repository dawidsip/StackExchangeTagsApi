# Stack Exchange Tags API
    This is a ``dotnet webapi`` project to showcase the usage of web applications developement paradigms:
        * __RESTFULL__ API
        * ORM for Database abstraction with in memory __Entity Framework__
        * Serialisation and deserialisation of __JSON__ data
        * Paging
        * Configuring
        * __Swagger__ for testing API calls
        * __OpenApi__
        * Data Persistance
        * Dependency Injection
        * abstraction
        * some __SOLID__ principles
        * Writing tests
        * Using __Moq__ for mocking objects for tests
        * Contenerisation with __Docker__ and Docker-Compose 

## Description
    The app fetches 1000 records of 'Tags' from the __StackExchange__ API and saves it in an 'in memory' Database, for later to be served with it's own API. The API is described with OpenApi and can be tested through __Swagger__.
## Usage & Installation
    Easiest way is by launching ``docker compose up`` or simply by running the project with ``dotnet run`` and going to the http://localhost:5184/swagger

    