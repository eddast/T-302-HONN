# Videotapes Galore: Convenient Management Tool for your Video Tapes!
Videotapes Galore is a management system suitable to small-to-medium sized video tape libraries for management and storing of records of video tape borrows, system users, video tape reviews and the system provides a personalized recommendation service to users. The system remains in development phase and no user interface is yet developed for the system, but it's functionality is implemented and is provided by a RESTful API implemented in C# with the .NET Core Framework. The API serves data via a remote connection to a MySQL database. The system was developed for the course T-302-HÖNN Software Development and Design by Alexander Björnsson and Edda Steinunn Rúnarsdóttir (Reykjavík University).

- [Usage](#usage)
  * [Remote Usage of API](#usage-remote)
  * [Building and Running API Locally](#usage-local)
- [Documentation](#documentation)
- [Major Deviations from Provided System Design](#design)
  * [Using .NET Core with C#](#dotnet)
  * [Usage of Three Layered Design over Using Only Service Classes](#layers)
  * [_WithBorrows_ Query Parameter in GET routes for Users and Video Tapes](#withborrows)
  * [The Initialization Routes](#initialization)
- [Known Limitations and Simplifications](#limitations)


<a name="usage"></a>
## Usage
TODO

<a name="usage-remote"></a>
### Remote Usage of API
TODO

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

Once web service is running, all routes in the system are available from the host http://localhost:5000. Once the web service is running, a thorough Swagger documentation explaining all API routes, their functionalities and how to query the API is available from http://localhost:5000/api/v1/documentation.

**Note** that on Linux-based operating systems both the build and run command can be run at the same time for convenience with dotnet build && dotnet run which builds the web service project and runs it at the same time. See example terminal command below:

```bash
username$ pwd
~/.../alexanderb13_eddasr15-P2/VideotapesGaloreAPI/VideotapesGaloreAPI.WebApi
username$ dotnet build && dotnet run
```

<a name="documentation"></a>
## Documentation
Developers decided on using Swagger as documentation tool for the API due to Swagger being a thorough documentation tool with good support for .NET core. In addition, minimal effort is required to generate a Swagger documentation thus documentation effort is minimized and productivity is enhanced. The Swagger documentation is generated from XML comments within the code which is why the project uses XML-like comments throughout for consistency. The Swagger document well describes the capabilities, (potential) access restrictions, return values, parameters and lists possible API responses for all routes in the system, so the user should not encounter difficulties using the API when navigating it with the help of the documentation. The documentation also clarifies all input models, data transfer objects returned from routes and parameters/body passed in to routes in terms of their types and attributes and gives example values for each such value potentially encountered using the API. Lastly, one can try querying the API via the Swagger documentation in an easy manner by clicking the "try it" options under each route which facilitates demonstration.

The API's Swagger documentation is available from the subroute /api/v1/documentation, e.g. at the URL http://localhost:5000/api/v1/documentation when the API is run locally and at the URL http://<hostname>/api/v1/documentation when accessed remotely.

<a name="design"></a>
## Major Deviations from Provided System Design
TODO

<a name="dotnet"></a>
### Using .NET Core with C#
TODO

<a name="layers"></a>
### Usage of Three Layered Design over Using Only Service Classes
TODO

<a name="initialization"></a>
### The Initialization Routes
TODO

<a name="limitations"></a>
## Known Limitations and Simplifications
TODO
