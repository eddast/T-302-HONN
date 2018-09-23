package io.swagger.api;

import io.swagger.model.*;
import io.swagger.api.DevelopersApiService;
import io.swagger.api.factories.DevelopersApiServiceFactory;

import io.swagger.annotations.ApiParam;
import io.swagger.jaxrs.*;

import io.swagger.model.Developer;
import io.swagger.model.Game;

import java.util.Map;
import java.util.List;
import io.swagger.api.NotFoundException;

import java.io.InputStream;

import org.glassfish.jersey.media.multipart.FormDataContentDisposition;
import org.glassfish.jersey.media.multipart.FormDataParam;

import javax.servlet.ServletConfig;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.SecurityContext;
import javax.ws.rs.*;
import javax.validation.constraints.*;

@Path("/developers")


@io.swagger.annotations.Api(description = "the developers API")
@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-13T18:18:20.475Z")
public class DevelopersApi  {
   private final DevelopersApiService delegate;

   public DevelopersApi(@Context ServletConfig servletContext) {
      DevelopersApiService delegate = null;

      if (servletContext != null) {
         String implClass = servletContext.getInitParameter("DevelopersApi.implementation");
         if (implClass != null && !"".equals(implClass.trim())) {
            try {
               delegate = (DevelopersApiService) Class.forName(implClass).newInstance();
            } catch (Exception e) {
               throw new RuntimeException(e);
            }
         } 
      }

      if (delegate == null) {
         delegate = DevelopersApiServiceFactory.getDevelopersApi();
      }

      this.delegate = delegate;
   }

    @POST
    
    @Consumes({ "application/json" })
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Add a new developer to the server", notes = "", response = Void.class, tags={ "developers", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 201, message = "Created", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 405, message = "Invalid input", response = Void.class) })
    public Response addDeveloper(@ApiParam(value = "Adds developer to server" ,required=true) Developer body
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.addDeveloper(body,securityContext);
    }
    @DELETE
    @Path("/{developerId}/games/{gameId}")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Assign developer from a game", notes = "De-registers developer as developer for a game", response = Void.class, tags={ "developers", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "Invalid ID supplied", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "Game or developer not found", response = Void.class) })
    public Response assignGameByIdFromDeveloperById(@ApiParam(value = "ID of developer to assign from game",required=true) @PathParam("developerId") Long developerId
,@ApiParam(value = "ID of game assign from developer",required=true) @PathParam("gameId") Long gameId
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.assignGameByIdFromDeveloperById(developerId,gameId,securityContext);
    }
    @POST
    @Path("/{developerId}/games/{gameId}")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Assign developer to a game", notes = "Registers developer as developer for a game", response = Void.class, tags={ "developers", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "Invalid ID supplied", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "Game or developer not found", response = Void.class) })
    public Response assignGameByIdToDeveloperById(@ApiParam(value = "ID of developer to assign to game",required=true) @PathParam("developerId") Long developerId
,@ApiParam(value = "ID of game assign to developer",required=true) @PathParam("gameId") Long gameId
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.assignGameByIdToDeveloperById(developerId,gameId,securityContext);
    }
    @DELETE
    @Path("/{developerId}")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Delete developer by ID", notes = "Deletes developer from server", response = Void.class, tags={ "developers", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "Successful Operation", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "Invalid ID supplied", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "Developer not found", response = Void.class) })
    public Response deleteDeveloperById(@ApiParam(value = "ID of developer to delete",required=true) @PathParam("developerId") Long developerId
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.deleteDeveloperById(developerId,securityContext);
    }
    @GET
    @Path("/{developerId}")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Find developer by ID", notes = "Returns a single developer", response = Game.class, tags={ "developers", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Game.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "Invalid ID supplied", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "Developer not found", response = Void.class) })
    public Response getDeveloperById(@ApiParam(value = "ID of developer to return",required=true) @PathParam("developerId") Long developerId
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.getDeveloperById(developerId,securityContext);
    }
    @GET
    
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Gets list of all developers", notes = "", response = Developer.class, responseContainer = "List", tags={ "developers", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Developer.class, responseContainer = "List") })
    public Response getDevelopers(@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.getDevelopers(securityContext);
    }
    @GET
    @Path("/{developerId}/games")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Find all games by developer by developer ID", notes = "Returns a list of games by developer", response = Game.class, responseContainer = "List", tags={ "developers", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Game.class, responseContainer = "List"),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "Invalid developer ID supplied", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "Developer not found", response = Void.class) })
    public Response getGamesByDeveloperById(@ApiParam(value = "ID of developer to find all games by",required=true) @PathParam("developerId") Long developerId
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.getGamesByDeveloperById(developerId,securityContext);
    }
    @PUT
    @Path("/{developerId}")
    @Consumes({ "application/json" })
    
    @io.swagger.annotations.ApiOperation(value = "Update developer by ID", notes = "", response = Void.class, tags={ "developers", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "Successful Operation", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "Invalid ID supplied", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "Developer not found", response = Void.class) })
    public Response updateDeveloperById(@ApiParam(value = "ID of developer to update",required=true) @PathParam("developerId") Long developerId
,@ApiParam(value = "Developer object to update" ,required=true) Developer body
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.updateDeveloperById(developerId,body,securityContext);
    }
}
