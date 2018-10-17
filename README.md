# Videotapes Galore: Convenient Management Tool for your Video Tapes!
Videotapes Galore is a management system suitable to small-to-medium sized video tape libraries that manages and holds records of video tape borrows, users, video tape reviews and provides a personalized recommendation service to users. The system remains in development phase and no user interface is yet developed for the system, but all functionality is provided by a RESTful API implemented in C# with the .NET Core Framework. The system was developed for the course T-302-HÖNN Software Development and Design by Alexander Björnsson and Edda Steinunn Rúnarsdóttir (Reykjavík University).

## Using the API Remotely
TODO

## Installing and Running the API Locally
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

Once web service is running, all routes in the system are available from the host http://localhost:5000. Once the web service is running, a thorough Swagger documentation explaining all API routes, their functionalities and how to query the API is available from http://localhost:5000/api/documentation.

**Note** that on Linux-based operating systems both the build and run command can be run at the same time for convenience with dotnet build && dotnet run which builds the web service project and runs it at the same time. See example terminal command below:

```bash
username$ pwd
~/.../alexanderb13_eddasr15-P2/VideotapesGaloreAPI/VideotapesGaloreAPI.WebApi
username$ dotnet build && dotnet run
```

## API Documentation and testing the API via Swagger
TODO

## Major Deviations from Provided System Design
TODO

### Using .NET Core with C#
TODO

### Usage of Three Layered Design over Using Only Service Classes
TODO

### _WithBorrows_ Query Parameter in GET routes for Users and Video Tapes
TODO

### The Initialization Routes
TODO
