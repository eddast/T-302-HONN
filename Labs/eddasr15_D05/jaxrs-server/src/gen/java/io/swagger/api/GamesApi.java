package io.swagger.api;

import io.swagger.model.*;
import io.swagger.api.GamesApiService;
import io.swagger.api.factories.GamesApiServiceFactory;

import io.swagger.annotations.ApiParam;
import io.swagger.jaxrs.*;

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

@Path("/games")


@io.swagger.annotations.Api(description = "the games API")
@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-13T18:18:20.475Z")
public class GamesApi  {
   private final GamesApiService delegate;

   public GamesApi(@Context ServletConfig servletContext) {
      GamesApiService delegate = null;

      if (servletContext != null) {
         String implClass = servletContext.getInitParameter("GamesApi.implementation");
         if (implClass != null && !"".equals(implClass.trim())) {
            try {
               delegate = (GamesApiService) Class.forName(implClass).newInstance();
            } catch (Exception e) {
               throw new RuntimeException(e);
            }
         } 
      }

      if (delegate == null) {
         delegate = GamesApiServiceFactory.getGamesApi();
      }

      this.delegate = delegate;
   }

    @POST
    
    @Consumes({ "application/json" })
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Add a new game to the store", notes = "", response = Void.class, tags={ "games", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 201, message = "Created", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 405, message = "Invalid input", response = Void.class) })
    public Response addGame(@ApiParam(value = "Game object that needs to be added to the store" ,required=true) Game body
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.addGame(body,securityContext);
    }
    @DELETE
    @Path("/{gameId}")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Delete game by ID", notes = "Deletes game from server", response = Void.class, tags={ "games", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "Successful Operation", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "Invalid ID supplied", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "Game not found", response = Void.class) })
    public Response deleteGameById(@ApiParam(value = "ID of game to delete",required=true) @PathParam("gameId") Long gameId
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.deleteGameById(gameId,securityContext);
    }
    @GET
    @Path("/{gameId}")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Find game by ID", notes = "Returns a single game", response = Game.class, tags={ "games", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Game.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "Invalid ID supplied", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "Game not found", response = Void.class) })
    public Response getGameById(@ApiParam(value = "ID of game to return",required=true) @PathParam("gameId") Long gameId
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.getGameById(gameId,securityContext);
    }
    @GET
    
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Gets list of all games", notes = "", response = Game.class, responseContainer = "List", tags={ "games", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Game.class, responseContainer = "List") })
    public Response getGames(@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.getGames(securityContext);
    }
    @PUT
    @Path("/{gameId}")
    @Consumes({ "application/json" })
    
    @io.swagger.annotations.ApiOperation(value = "Update game by ID", notes = "", response = Void.class, tags={ "games", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "Successful Operation", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "Invalid ID supplied", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "Game not found", response = Void.class) })
    public Response updateGameById(@ApiParam(value = "ID of game to update",required=true) @PathParam("gameId") Long gameId
,@ApiParam(value = "Game object to update" ,required=true) Game body
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.updateGameById(gameId,body,securityContext);
    }
}
