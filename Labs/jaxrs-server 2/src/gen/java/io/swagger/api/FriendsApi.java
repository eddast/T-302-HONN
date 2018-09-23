package io.swagger.api;

import io.swagger.model.*;
import io.swagger.api.FriendsApiService;
import io.swagger.api.factories.FriendsApiServiceFactory;

import io.swagger.annotations.ApiParam;
import io.swagger.jaxrs.*;

import io.swagger.model.Friend;
import io.swagger.model.FriendInput;

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

@Path("/friends")


@io.swagger.annotations.Api(description = "the friends API")
@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-16T17:01:38.449Z")
public class FriendsApi  {
   private final FriendsApiService delegate;

   public FriendsApi(@Context ServletConfig servletContext) {
      FriendsApiService delegate = null;

      if (servletContext != null) {
         String implClass = servletContext.getInitParameter("FriendsApi.implementation");
         if (implClass != null && !"".equals(implClass.trim())) {
            try {
               delegate = (FriendsApiService) Class.forName(implClass).newInstance();
            } catch (Exception e) {
               throw new RuntimeException(e);
            }
         } 
      }

      if (delegate == null) {
         delegate = FriendsApiServiceFactory.getFriendsApi();
      }

      this.delegate = delegate;
   }

    @POST
    
    @Consumes({ "application/json" })
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Add a new friend to system", notes = "", response = Void.class, tags={ "friends", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 201, message = "Created", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 405, message = "Invalid input", response = Void.class) })
    public Response addFriend(@ApiParam(value = "Friend object to be added to system" ,required=true) FriendInput body
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.addFriend(body,securityContext);
    }
    @DELETE
    @Path("/{id}")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Deletes friend by id", notes = "Deletes friend by id", response = Void.class, tags={ "friends", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "invalid id supplied", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "friend not found", response = Void.class) })
    public Response deleteFriendById(@ApiParam(value = "id of friend to delete",required=true) @PathParam("id") Long id
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.deleteFriendById(id,securityContext);
    }
    @GET
    @Path("/{id}")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Gets specific friend by id", notes = "", response = Friend.class, tags={ "friends", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Friend.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "invalid id supplied", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "friend not found", response = Void.class) })
    public Response getFriend(@ApiParam(value = "id of friend to get",required=true) @PathParam("id") Long id
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.getFriend(id,securityContext);
    }
    @GET
    
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Gets list of all friends", notes = "", response = Friend.class, responseContainer = "List", tags={ "friends", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Friend.class, responseContainer = "List") })
    public Response getFriends(@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.getFriends(securityContext);
    }
    @PUT
    @Path("/{id}")
    @Consumes({ "application/json" })
    
    @io.swagger.annotations.ApiOperation(value = "Update friend by ID", notes = "", response = Void.class, tags={ "friends", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "invalid id supplied", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "friend not found", response = Void.class) })
    public Response updateGameById(@ApiParam(value = "id of friend to delete",required=true) @PathParam("id") Long id
,@ApiParam(value = "friend object to update" ,required=true) FriendInput body
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.updateGameById(id,body,securityContext);
    }
}
