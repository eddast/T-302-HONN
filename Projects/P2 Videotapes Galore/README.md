# Videotapes Galore: Convenient Management Tool for your Video Tapes!
Videotapes Galore is a management system suitable for small-to-medium sized video tape libraries. The system is intended for the management and storing of records of video tape borrows, system users and video tape reviews while providing a personalized video tape recommendation service to each user. 

The system remains in development phase and no user interface is yet developed for the system. It's functionality is provided by a RESTful API implemented in C# with the .NET Core Framework. The API stores data via remote connection to a MySQL database located on an Amazon AWS server. 

The system was and designed and developed by Alexander Björnsson and Edda Steinunn Rúnarsdóttir for the course T-302-HONN Software Development (Reykjavík University).

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
One goal of developing the system was to make it possible for an outside user to try out the API without needing both the correctly built source code and a configured runtime environment. The system has therefore been deployed to a remote Amazon AWS cloud server and  is up and running at the URL http://35.176.20.49:1337/api/v1.

Note that using the API remotely is faster due to both the API and MySQL servers being hosted by Amazon AWS. Being hosted by the same web service allows data to be routed more efficiently between the two servers than not, e.g. if the API server was run on a local machine separate from Amazon AWS. 

In addition to this, the remote .NET API  runs in production mode instead of development mode which is generally faster. Using the remote API is therefore recommended for an optimal user experience.

Refer to the Swagger documentation at http://35.176.20.49:1337/api/v1/documentation for a thorough explanation of API routes and their functionality.

<a name="usage-local"></a>
### Building and Running API Locally
All source code and dependencies for the web service API resides in the project './VideotapesGaloreAPI'. More specifically, the web service project itself resides in the project './VideotapesGaloreAPI/VideotapesGalore.WebApi'.

The .NET SDK is required for building and running the API locally. ([click here](https://www.microsoft.com/net/learn/dotnet/hello-world-tutorial#install "Install the .NET SDK") for an installation guide for installing the .NET SDK and running the dotnet shell command).

Once this prerequisite is met, run the command dotnet build in terminal within the folder containing the web service project. See example terminal command below:

```bash
username$ pwd
~/.../alexanderb13_eddasr15-P2/VideotapesGaloreAPI/VideotapesGaloreAPI.WebApi
username$ dotnet build
```

Once the project has successfully been built, run the server locally with the command dotnet run. See example terminal command below:

```bash
username$ pwd
~/.../alexanderb13_eddasr15-P2/VideotapesGaloreAPI/VideotapesGaloreAPI.WebApi
username$ dotnet run
```

Once web service is running, all routes in the system are available from the host http://localhost:5000. Once the web service is running, a thorough Swagger documentation explaining all API routes and their functionality is available from http://localhost:5000/api/v1/documentation. Refer to this documentation to navigate the API.

**Note** that on Linux-based operating systems both the build and run command can be run at the same time for convenience with dotnet build && dotnet run which both builds and executes the web service. See example terminal command below:

```bash
username$ pwd
~/.../alexanderb13_eddasr15-P2/VideotapesGaloreAPI/VideotapesGaloreAPI.WebApi
username$ dotnet build && dotnet run
```

<a name="documentation"></a>
## Documentation
The developers of this project decided on using Swagger as documentation tool for the API due to Swagger being a thorough documentation tool with good support for .NET core. In addition, generating a Swagger documentation is simple and thusly the documentation effort is minimized and productivity is enhanced. 

The Swagger documentation is generated from XML comments within the code which is why the project uses XML-like comments throughout for consistency. The Swagger document describes the capabilities, potential access restrictions, return values, parameters and lists possible API responses for all routes in the system. 

The Swagger documentation provides all necessary information for a user when navigating the system's API by formalizing all input models and data transfer objects returned from routes along with any models passed in to routes in terms of their types and attributes.

Lastly, one can try querying the API via the Swagger documentation in an easy manner by clicking the "try it" option under each route which provides a demonstration.

The API's Swagger documentation is available from the subroute /api/v1/documentation, e.g. at the URL http://localhost:5000/api/v1/documentation when the API is run locally or at the URL http://35.176.20.49:1337/api/v1/documentation when accessed remotely.

<a name="testing"></a>
## Testing

<a name="unit-testing"></a>
### Unit Testing
Both the branch coverage and line coverage for unit tests for the system is over 90% which is an optimal coverage in terms of balancing effort and efficiency of unit tests. The unit tests cover only the buisness logic part of the system (e.g. the service classes) because controllers and repositories have almost none internal logic and are therefore more suited to be tested via integration tests for functionality.

Executing tests and providing code coverage reporting was achieved via the code coverage tool **Coverlet** and the code coverage report generator tool **ReportGenerator** for .NET Core. The prerequisite to running tests and outputting a code coverage report is installing both these tools. This can be done directly via the dotnet terminal command. See below example terminal commands for a reference on how to install both tools.

Installing Coverlet:
```bash
username$ dotnet tool install --global coverlet.console
```

Installing ReportGenerator:
```bash
username$ dotnet tool install -g dotnet-reportgenerator-globaltool
```

After both tools are installed, run the script **./test.sh** located in the VideotapesGaloreAPI.UnitTests project to build source code, run tests and output a code coverage report. It may be necessary to make the script executable by using chmod +x on the script file before running it. See example terminal command for running the test script:

```bash
username$ pwd
~/.../alexanderb13_eddasr15-P2/VideotapesGaloreAPI/VideotapesGaloreAPI.UnitTests
username$ chmod +x test.sh
username$ ./test.sh
```
Once the script finishes execution, a HTML code coverage report is generated and can be viewed using a browser (or any other tool able to render HTML documents). The code coverage report resides at the path _~/.../alexanderb13_eddasr15-P2/VideotapesGaloreAPI/VideotapesGaloreAPI.UnitTests/coverage/index.htm_.

The following image demonstrates what the unit test coverage report should look like for reference:

![alt text](https://image.ibb.co/gCpGiA/Videotapes-Galore-Unit-Tests-Code-Coverage-Report-Demo.png "Code Coverage Report Sample for Unit Tests")

<a name="integration-testing"></a>
### Integration Testing
The integration tests for system were implemented via the **xunit** testing framework for .NET core and can be run with the dotnet test command when within the integration test project. See example command below:

```bash
username$ pwd
~/.../alexanderb13_eddasr15-P2/VideotapesGaloreAPI/VideotapesGaloreAPI.IntegrationTests
username$ dotnet test
```

The integration test project initialises a test context. The initialisation of the test context resides under the subdirectory /Context within the integration project (VideotapesGaloreAPI.IntegrationTests/Context/). The initialisation process seeds the data source with initial known values that can be used to conduct tests, i.e. the context initialises three known users and three known tapes along with borrow records for these users and tapes into system. This is so that users and/or tapes do not have to renewed and added for every test that requires using some user or tape resources to perform some functionality which would result in repeated code. The known resources that are created in the test context initialisation process are then deleted and disposed of when all integration tests have run.

The integration test environment test against the live MySQL database that used for the real web service. This design decision was made so that the integration test results would be more accurate in terms of interaction in respect to the actual data source, potentially exposing more explicit and relevant errors in system. This is also the reason for carefully disposing the seeded content for the test context after tests are run, so that data source of system is in the same state after tests run as it was before test were run.

The integration tests for system are implemented as follows:

1. **Safe route tests**: integration test set located within the class GetRoutesTests. The test set is a very lightweight integration test that simply verifies that all safe routes (GET routes) in system return a status code of 200 (OK) and data in JSON format

2. **CRUD tests**: integration sets that test CRUD-functionalities (create, read, update, delete) of resources that support CRUD-functionalities. The implementation of the CRUD test sets are made generic by the template method strategy pattern  — an abstract class implementing the ICRUDTest interface implements basic functionality of CRUD testing, e.g. reading, adding, updating and deleting   which derived classes augment by implementing virtual functionalities. The derived classes and their corresponding tested resources are:

* UserCRUDTests which test CRUD functionalities of user resources

* TapeCRUDTests which test CRUD functionalities of tape resources

* ReviewCRUDTests which test CRUD functionalities of review resources.

These CRUD tests are quite extensive as integration tests and are similar to end-to-end tests, but this decision was deliberate as despite the tests being quite extensive, the errors potentially detected by these tests are still quite isolated to a small subset of classes and are usually only restricted to the service class for the corresponding resource.

3. **Borrow record tests**: integration test set located within the class BorrowRecordTests that relies on seeded data and tests the functionalities of tape and user relations i.e. borrows. This includes registering tapes on loan, returning tapes and viewing borrow history for users.
4. **Loan date and loan duration tests**: integration test set located within the class ReportTests that tests if the filter for the GET routes for list of all tapes and list of all users functions as expected using seeded content.

<a name="design"></a>
## Deviations from Provided System Design

<a name="dotnet"></a>
### Using .NET Core with C#
The provided system design suggested implementing the project in the Java programming language using Maven to aid the build process and using Jetty as web server container to obtain the functionality of the API. However, the authors of this API decided on implementing the API using the programming language C# and the .NET core framework.

This was due to the authors having more experience in the .NET Core framework over using Jetty containers and because the authors felt that using Java with Jetty had no obvious significant benefits over using C# with .NET Core, especially since C# and Java have similar capabilities, both being strongly typed and object-oriented. Moreover, the .NET Core framework has good documentation, excellent support for automated Swagger documentation generation (via the *Swashbuckle* package), facilitated automatic mapping of data types (via *AutoMapper* package) as well as a very simplified mocking mechanism for unit testing (via *Moq* and *NBuilder* packages). 

The authors found all of the facts listed to be real benefits in favour of .NET Core. Therefore for simplification, efficiency and to minimise hindrance, C# and .NET Core was chosen for the system implementation.

Despite the project not using the Java programming language, Java being the choice platform for the course, various design patterns are still explored and used in the project. The most notable example is that all classes aim to implement to interfaces for low coupling, testability and maintainability. All class dependencies are injected using constructor injection. Instances of concrete classes are assigned to their interfaces in Startup.cs of the WebApi project which decides dependencies at runtime as per the plugin design pattern, making use of the strategy design pattern. 

Moreover, a Gateway is used for a connection to the remote database and the main concept of factories (e.g. using singleton instances) are exploited via assigning singleton as opposed to transient instances of concrete classes where appropriate to their interfaces. All classes aim to have a single responsibility as per the single responsibility principle and service classes conduct buisness logic relevant only to the type of resources they represent in the API. 

The next section discusses layered and service-oriented design for the system.

<a name="layers"></a>
### Three Layered Design over Service Classes Only
Although the design provided suggested only service classes for all business logic and fetching data, the authors of this system felt that it was more appropriate to use a three layered system to decouple the system as well as possible so that the system's components would be more maintainable, testable and provide better isolation of any errors.

The system's **presentation layer**, contained within the VideotapesGalore.WebApi project consists of two distinct controller classes. One for routes prefixed with tapes and one for routes prefixed with users. These controllers define the endpoints for the API e.g. the routes for the system and are extensively documented with XML-like comments for automatic Swagger documentation generation. Functions within the presentation layer controllers expose users to the API; accept and validate input (HTTP requests to routes) from client and return back an appropriate response when client’s request has been resolved.

The project’s **domain layer** is the VideotapesGalore.Services project whose classes implement all business logic for the system. Any functions for error checking and general logic needing to be conducted for obtaining correct functionality are located within the service classes of this project.

The project’s **data source layer** is essentially partitioned into two separate sections: the data source connector and the repository classes. The former connects to remote MySql database and acts as a gateway to the remote source. The authors felt the need for having a repository layer to hide entity models (i.e. the raw data format from database containing redundant fields such as “last modified” and “created at” fields that are of no relevance to the system) from the service layer. Therefore the purpose of these repository layers are essentially only to fetch data from or add data to the data source and translating data from their raw entity representation to a DTO (data transfer object) before passing it down to the service layer or to database. Each repository class handles functionality and mapping of resource for a single table in database only. This design implements the domain model pattern.

See component diagrams below that depict the difference between the initial design idea provided and the format for the actual implementation of the system using the three layer model (note that the presentation layer classes i.e. controller classes are omitted from diagram as their connection to rest of the classes is trivial and only to service classes).

Provided design idea:

![alt text](https://image.ibb.co/bsqk6L/Initial-Design-Concept.png "Initial Provided Design Idea")

Actual implementation:

![alt text](https://image.ibb.co/cHUdRL/Final-Design-Concept.png "Actual Implementation")

<a name="initialization"></a>
### The Seed Routes
To make the system restorable while development was still in progress, two routes were implemented additionally to those specified in the provided deisng. Both have the purpose of initialising the system, e.g. seeding the data with initial predefined data stored locally within project. 

When either route is called, a reader class reads in corresponding JSON data from a local JSON file and adds data to data source. The route /api/v1/tapes/seed initialises all tapes and the route /api/v1/users/seed initialises all user and borrow records in system 
**(Note that the former must be called before the latter because of the database's relational model).**

These routes are highly restricted and really only meant to be accessible for system authors and not even administrators, Since they are meant for development facilitation. A simple authorisation mechanism has therefore been implemented for these two routes to prevent any misuse. If the he client does not fulfil the authorisation requirement when these routes are called, the client gets a response of **401 (Unauthorised)**. Means of authorisation will not be exposed in this document for security purposes but can be requested from system authors if needed (contact authors via email: eddasr15@ru.is or alexanderb13@ru.is).

<a name="limitations"></a>
## Known Limitations and Simplifications
The scope of this project does not cover the system’s functionality to the full extent of it’s conceptual design idea. This section covers omitted functionalities of the system for simplification purposes for the scope of the project.

No **user interface** is provided for the system although specified in provided design. Only the API design and functionality is provided since a user interface design on top of the functionality would extend beyond this project's scope.

No **user authentication** is present in the system. All system users have the same access rights to all resources. This of course should not be the case as the system design defines three types of users with three distinct roles based on access rights. However, a very simple and minimal authorisation mechanism was implemented for the two seed routes (see above section) but since that was only for development purposes the mechanism is very minimal and insecure and therefore was not considered viable as a solution user authentication.

No **integration to HubSpot and Google Analytics** is present in system although connection to both tools is present in the design idea to build a UI and to provide a smarter recommendation service with real time analytics. This integration would extend beyond the project scope.

<a name="recommendation"></a>
### Logic for Tape Recommendations
Since Google Analytics integration is omitted, the recommendation service had to be implemented using existing resources and functionalities. Therefore the authors decided on basing that logic on three main principles, all the while excluding tapes that are unavailable (e.g. currently on loan) and tapes that requesting user has already borrowed. 

The three main principles recommendations are listed here, going down in order of precedence if a principle fails to apply:

1. **Common Borrows Principle**: The system attempts to base tape recommendations on common borrows between users. Similar people like similar things. If two users have a similar history borrowing history, they get tapes from each others borrow history recommended to each other. 

2. **Ratings Principle**: The highest rated tapes are recommended to the user.

3. **Newest Releases Principle**: The newest released tape in system is recommended to user. 

If the newest releases principle fails to provide recommendation as well, it explicitly tells us that there are no tapes in system left that are available (not on loan) that the user has not yet borrowed. An error is sent back as no viable recommendation can be provided for the user.
