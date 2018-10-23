# Videotapes Galore: Convenient Management Tool for your Video Tapes!
Videotapes Galore is a management system suitable to small-to-medium sized video tape libraries for management and storing of records of video tape borrows, system users, video tape reviews and the system provides a personalized recommendation service to users. The system remains in development phase and no user interface is yet developed for the system, but it's functionality is implemented and is provided by a RESTful API implemented in C# with the .NET Core Framework. The API serves data via a remote connection to a MySQL database that is hosted by an Amazon AWS server. The system was developed for the course T-302-HÖNN Software Development and Design by Alexander Björnsson and Edda Steinunn Rúnarsdóttir (Reykjavík University).

- [Usage](#usage)
  * [Remote Usage of API (Recommended)](#usage-remote)
  * [Building and Running API Locally](#usage-local)
- [Documentation](#documentation)
- [Testing](#testing)
  * [Unit Testing](#unit-testing)
  * [Integration Testing](#integration-testing)
- [Deviations from Provided System Design](#design)
  * [Using .NET Core with C#](#dotnet)
  * [Three Layered Design over Service Classes Only](#layers)
  * [The Seed Routes](#initialization)
- [Known Limitations and Simplifications](#limitations)
  * [Logic for Tape Recommendations](#recommendation)


<a name="usage"></a>
## Usage

<a name="usage-remote"></a>
### Remote Usage of API (Recommended)
The authors of this system aimed to make sure that no complications would arise for an outsider attempting to use the API due to possible runtime environment problems or lack of tools to build or run the project. In addition to this, the API ran quite slow when run locally due to both the project and the MySQL connection running in development mode. Therefore to reduce complications for potential users of system and to enhance the speed and efficiency of the API the project was deployed with the help of Amazon AWS. The system is therefore up and running at the remote URL http://35.176.20.49:1337/api/v1 (note that this is of course only the base URL so this URL is not valid as standalone, but adding sub URLs to it will envoke functionalities of the API, f.x. http://35.176.20.49:1337/api/v1/tapes gets all tapes in system).

Note that using the API remotely is faster and more efficient due to the server that runs the API remotely and the server that maintains the MySQL database are both hosted with Amazon AWS services. Therefore requests take less time to go to and from API to data source as they are routed in a smarter way by Amazon than when the API runs locally. In addition to this, the .NET project runs in production build mode instead of debug mode which is generally faster and a more optimized run mode. Therefore we recommend remote usage of the API.

Refer to the thorough Swagger documentation explaining all API routes, their functionalities and how to query the API from http://35.176.20.49:1337/api/v1/documentation for a proper instructions on how to use the API and it’s capabilities.

<a name="usage-local"></a>
### Building and Running API Locally
All source code and dependencies for the web service API resides in the folder './VideotapesGaloreAPI'. More specifically, the web service project itself resides in the folder './VideotapesGaloreAPI/VideotapesGalore.WebApi'.

In order to install and build the API, one must have dotnet (.NET SDK) installed on one's computer ([click here](https://www.microsoft.com/net/learn/dotnet/hello-world-tutorial#install "Install the .NET SDK") for an installation guide for installing the dotnet shell command).

Once that prerequisite is met, run the command dotnet build in terminal within the folder containing the web service project. See example terminal command below:

```bash
username$ pwd
~/.../alexanderb13_eddasr15-P2/VideotapesGaloreAPI/VideotapesGaloreAPI.WebApi
username$ dotnet build
```

Once the project has successfully been built, one must run the server to obtain the functionality of the system with the command dotnet run. See example terminal command below:

```bash
username$ pwd
~/.../alexanderb13_eddasr15-P2/VideotapesGaloreAPI/VideotapesGaloreAPI.WebApi
username$ dotnet run
```

Once web service is running, all routes in the system are available from the host http://localhost:5000. Once the web service is running, a thorough Swagger documentation explaining all API routes, their functionalities and how to query the API is available from http://localhost:5000/api/v1/documentation. Refer to this documentation to navigate the API.

**Note** that on Linux-based operating systems both the build and run command can be run at the same time for convenience with dotnet build && dotnet run which builds the web service project and runs it at the same time. See example terminal command below:

```bash
username$ pwd
~/.../alexanderb13_eddasr15-P2/VideotapesGaloreAPI/VideotapesGaloreAPI.WebApi
username$ dotnet build && dotnet run
```

<a name="documentation"></a>
## Documentation
Developers decided on using Swagger as documentation tool for the API due to Swagger being a thorough documentation tool with good support for .NET core. In addition, minimal effort is required to generate a Swagger documentation thus documentation effort is minimized and productivity is enhanced. The Swagger documentation is generated from XML comments within the code which is why the project uses XML-like comments throughout for consistency. The Swagger document well describes the capabilities, (potential) access restrictions, return values, parameters and lists possible API responses for all routes in the system, so the user should not encounter difficulties using the API when navigating it with the help of the documentation. The documentation also clarifies all input models, data transfer objects returned from routes and parameters/body passed in to routes in terms of their types and attributes and gives example values for each such value potentially encountered using the API. Lastly, one can try querying the API via the Swagger documentation in an easy manner by clicking the "try it" options under each route which facilitates demonstration.

The API's Swagger documentation is available from the subroute /api/v1/documentation, e.g. at the URL http://localhost:5000/api/v1/documentation when the API is run locally and at the URL http://35.176.20.49:1337/api/v1/documentation when accessed remotely.

<a name="testing"></a>
## Testing

<a name="unit-testing"></a>
### Unit Testing
Both the branch coverage and line coverage for unit tests for the system is over 90% which is an optimal coverage in terms of balancing effort and efficiency of unit tests. The unit tests cover only the buisness logic part of the system (e.g. the service classes) because controllers and repositories have almost none internal logic and are therefore more suited to be tested via integration tests for functionality.

Building, running unit tests on the project and outputting a code coverage report was made easy via the code coverage tool **Coverlet** for .NET Core and the code coverage report generator tool **ReportGenerator** for .NET Core. The prerequisite to running tests and outputting a code coverage report is installing both these tools. This can be done directly via the dotnet terminal command. See below example terminal commands for a reference on how to install both tools.

Installing Coverlet:
```bash
username$ dotnet tool install --global coverlet.console
```

Installing ReportGenerator:
```bash
username$ dotnet tool install -g dotnet-reportgenerator-globaltool
```

After both tools are installed, to build, test and output code coverage report on project one must navigate to the unit test project of the source code and run the script **./test.sh** located in the VideotapesGaloreAPI.UnitTests folder. Note that if when running the script one gets permission denied error, one may need to run chmod +x on the script file before running it. See example terminal command for running the test script:

```bash
username$ pwd
~/.../alexanderb13_eddasr15-P2/VideotapesGaloreAPI/VideotapesGaloreAPI.UnitTests
username$ chmod +x test.sh
username$ ./test.sh
```
Once this command has been executed and tests have been run, a code coverage report is generated on a HTML form and can be viewed using browser (or other tools used display HTML documents). The code coverage report resides at the path _~/.../alexanderb13_eddasr15-P2/VideotapesGaloreAPI/VideotapesGaloreAPI.UnitTests/coverage/index.htm_.

The following image demonstrates what the unit test coverage report should look like for reference:

![alt text](https://image.ibb.co/gCpGiA/Videotapes-Galore-Unit-Tests-Code-Coverage-Report-Demo.png "Code Coverage Report Sample for Unit Tests")

<a name="integration-testing"></a>
### Integration Testing
TODO

<a name="design"></a>
## Deviations from Provided System Design

<a name="dotnet"></a>
### Using .NET Core with C#
The provided system design suggested implementing the project in the Java programming language using Maven to aid the build process and using Jetty as web server container to obtain the functionality of the API. However, the authors of this API decided on implementing the API using the programming language C# and the .NET core framework.

This was due to the authors having more experience in the .NET Core framework over using Jetty containers and because the authors felt that using Java with Jetty had no significant benefits over using C# with .NET Core, especially since both C# and Java are strongly typed and object-oriented so both have similar capabilities. Moreover, the .NET Core framework has well documented, good support for automated Swagger documentation generation (via the *Swashbuckle* package), facilitated automatic mapping of data types (via *AutoMapper* package) as well as very simplified mocking mechanism for unit testing (via *Moq* and *NBuilder* packages), all which authors found to be real benefits in favour of .NET Core. Therefore for simplification, efficiency and to minimise hindrance C# and .NET Core was chosen for the implementation.

Despite the project not using the Java programming language, the general the programming language used in course, various design patterns are still explored and used in the project. The most notable example is that all classes aim to implement to interfaces for low coupling, testability and maintainability and all class dependencies are injected using constructor injection (note that although not evident for those not accustomed to C#, instances of concrete classes are assigned to their interfaces in Startup.cs of the WebApi project which decides the dependencies at runtime as per the plugin design pattern that are injected for when the WebApi is run) which is a design that clearly exploits the strategy design pattern. Moreover, a Gateway is used for a connection to the remote database and the main concept of factories (e.g. using singleton instances) are exploited via assigning singleton as opposed to transient instances of concrete classes where appropriate to their interfaces. All classes aim to have a single responsibility as per the single responsibility principle and service classes conduct buisness logic relevant only to the type of resources they represent in the API. The implementation is also layered and service-oriented as well, which is explained in more detail in next section.

<a name="layers"></a>
### Three Layered Design over Service Classes Only
Although design provided suggested only service classes for all business logic and fetching data, the authors of this system felt that it was more appropriate to use the three layered system to decouple the system as well as possible so that the system components would be more maintainable, testable and provide better isolation of any errors.

The project’s **presentation layer** is contained within the VideotapesGalore.WebApi project and are two distinct controller classes, one for routes prefixed with tapes and one for routes prefixed with users. Said controller classes define the endpoints for the API e.g. the routes for the system and are extensively documented with XML-like comments for automatic Swagger documentation generation. The purposes of functions within these controller classes are exposing the user to the API, accepting input (HTTP requests to routes) from client, validate client's input format and sending back an appropriate response when client’s request has been resolved.

The project’s **domain layer** is the VideotapesGalore.Services project whose classes implement all business logic for the system. Any functions, search, error checking and general logic that needs to be conducted for obtaining correct functionality is done within the service classes of this project.

The project’s **data source layer** is essentially partitioned in two sections: the data source connector class and repository classes. The former connects to remote MySql database and acts as a gateway to the remote source. The authors felt the need for having a repository layer to hide entity models (i.e. the raw data format from database containing redundant fields such as “last modified” and “created at” fields that are of no relevance to the system) from the service layer. Therefore the sole purpose of these repository layers are essentially only to fetch data from or add data to the data source and translating data from their raw entity representation to a DTO (data transfer object) before passing it down to the service layer or to database. Each repository class handles functionality and mapping of resource for a single table in database only, and this concept essentially implements the domain model pattern.

See component diagrams below that depict the difference between the initial design idea provided and the format for the actual implementation of the system using the three layer model (note that the presentation layer classes i.e. controller classes are omitted from diagram as their connection to rest of the classes is trivial and only to service classes).

Provided design idea:

![alt text](https://image.ibb.co/bsqk6L/Initial-Design-Concept.png "Initial Provided Design Idea")

Actual implementation:

![alt text](https://image.ibb.co/cHUdRL/Final-Design-Concept.png "Actual Implementation")

<a name="initialization"></a>
### The Seed Routes
To make the system easily manipulatable and resettable while development was still in progress, two additional routes to those specified in design provided were implemented. Both have the purpose of initialising the system, e.g. seeding the data with initial predefined data stored locally within project. When either route is called, a reader class reads in corresponding JSON data from local JSON file and adds data to data source. The route /api/v1/tapes/seed initialises all tapes in route and the route /api/v1/users/seed initialises all users and borrow records in system - note that the former must be called before the other because when no tapes are in system, registering any borrow record will fail due to tape not being in system.

Note however that since this route is for facilitating of development only they are highly restricted and really only meant to be accessible for system authors and not even administrators. Therefore a simple authorisation mechanism has been implemented for these two routes only to prevent any potential malicious user from using this cautious routes and to prevent users from making requests to the routes by mistake. When the seed routes are called and client does not fulfil the secret authorisation requirement client gets a response of of 401 (Unauthorised). This requirement will not be exposed in this document for security purposes but can be requested from system authors if needed (contact authors via email: eddasr15@ru.is or alexanderb13@ru.is).

<a name="limitations"></a>
## Known Limitations and Simplifications
The scope of this project does not cover the VideotapesGalore system’s functionality to the full extent of it’s conceptual design idea. This section covers omitted functionalities of the system for simplification purposes for the scope of the project.

No **user interface** is provided for the system although specified in provided design. Only the API design and functionality is provided since a user interface design on top of the functionality would extend beyond this project's scope.

No **user authentication** is present in the system, so no users are authenticated into the system and all system users have the same access rights to all resources. This of course should not be the case as the system design defines three types of users with three distinct roles based on access rights. However, very simple and minimal authorisation mechanism was implemented for the two seed routes (see above section) but since that was only for development purposes the mechanism is very minimal and insecure and therefore was not considered viable as a solution for an authorisation of users.

No **integration to HubSpot and Google Analytics** is present in system although connection to both tools is present in the design idea to build a UI and to provide a smarter recommendation service with real time analytics. This integration would extend beyond the project scope.

<a name="recommendation"></a>
### Logic for Tape Recommendations
Since Google Analytics integration is omitted, the recommendation service had to be implemented using existing resources and functionalities. Therefore the authors decided on basing that logic on three main principles all the while excluding tapes that are unavailable (e.g. currently on loan) and tapes that user requesting recommendation has already borrowed. The three main principles of a recommendation in the system are:

1. **Common Borrows Principle**: Has the highest precedence. The system attempts to base tape recommendation on common borrows, i.e. if the user that's requesting recommendation has borrowed some of the same tapes as some other user in system, the user requesting recommendation is likely to appreciate some other tapes of that user's borrow history since they have a tape borrow in common (this of course requires that some common borrower exists for user). Therefore tapes by common borrows are returned as recommendation if any exist that are available (i.e. not currently on loan) and user requesting recommendation has not borrowed them before.

2. **Ratings Principle**: Has the second highest precedence. If the common borrows principle fails to provide recommendation based on common borrows, the highest rated tape in system that user has not yet borrowed is recommended to user. (This of course requires ratings to be present for tapes in system and this requires that user has not borrowed rated tapes in system).

3. **Newest Releases Principle**: Has the lowest precedence. If the ratings principle fails to provide recommendation based on ratings, the newest released tape in system that user has not seen is recommended to user. This will provide recommendation without fail IF any tapes exist in system that are available (not currently being borrowed) that user requesting recommendation has not yet borrowed.

If the newest releases principle fails to provide recommendation as well, it explicitly tells us that there are no tapes in system left that are available (not on loan) and that user has not yet borrowed. Therefore in this case an error is sent back as no viable recommendation can be provided for the user.
