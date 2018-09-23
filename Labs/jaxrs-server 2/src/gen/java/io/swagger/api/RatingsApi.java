package io.swagger.api;

import io.swagger.model.*;
import io.swagger.api.RatingsApiService;
import io.swagger.api.factories.RatingsApiServiceFactory;

import io.swagger.annotations.ApiParam;
import io.swagger.jaxrs.*;

import io.swagger.model.Rating;
import io.swagger.model.RatingInput;

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

@Path("/ratings")


@io.swagger.annotations.Api(description = "the ratings API")
@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-16T17:01:38.449Z")
public class RatingsApi  {
   private final RatingsApiService delegate;

   public RatingsApi(@Context ServletConfig servletContext) {
      RatingsApiService delegate = null;

      if (servletContext != null) {
         String implClass = servletContext.getInitParameter("RatingsApi.implementation");
         if (implClass != null && !"".equals(implClass.trim())) {
            try {
               delegate = (RatingsApiService) Class.forName(implClass).newInstance();
            } catch (Exception e) {
               throw new RuntimeException(e);
            }
         } 
      }

      if (delegate == null) {
         delegate = RatingsApiServiceFactory.getRatingsApi();
      }

      this.delegate = delegate;
   }

    @DELETE
    @Path("/users/{userId}/tapes/{tapeId}")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Delete given user's rating for a specific tape", notes = "Delete given user's rating for a specific tape", response = Void.class, tags={ "ratings", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "invalid id supplied for video tape or user", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "user rating not found on video tape", response = Void.class) })
    public Response deleteUserRatingByTapeId(@ApiParam(value = "Id of user deleting his or her rating on video tape",required=true) @PathParam("userId") Long userId
,@ApiParam(value = "Id of video tape user is deleting his or her rating on",required=true) @PathParam("tapeId") Long tapeId
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.deleteUserRatingByTapeId(userId,tapeId,securityContext);
    }
    @GET
    @Path("/users/{userId}/tapes/{tapeId}")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Gets given user's rating for a specific tape", notes = "Gets given user's rating for a specific tape", response = RatingInput.class, tags={ "ratings", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = RatingInput.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "invalid id supplied for video tape or user", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "video tape or user not found", response = Void.class) })
    public Response getUserRatingByTapeId(@ApiParam(value = "Id of user viewing his or her rating on video tape",required=true) @PathParam("userId") Long userId
,@ApiParam(value = "Id of video tape user is viewing his or her rating on",required=true) @PathParam("tapeId") Long tapeId
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.getUserRatingByTapeId(userId,tapeId,securityContext);
    }
    @GET
    @Path("/users/{userId}")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Gets all ratings by given user on tapes", notes = "Gets all ratings by given user on tapes", response = Rating.class, responseContainer = "List", tags={ "ratings", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Rating.class, responseContainer = "List"),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "invalid id supplied for user", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "user not found with id", response = Void.class) })
    public Response getUserRatings(@ApiParam(value = "Id of user viewing his or her ratings on all video tapes",required=true) @PathParam("userId") Long userId
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.getUserRatings(userId,securityContext);
    }
    @POST
    @Path("/users/{userId}/tapes/{tapeId}")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Add given user's rating for a specific tape", notes = "Add given user's rating for a specific tape", response = Void.class, tags={ "ratings", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "invalid id supplied for video tape or user", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "video tape or user not found", response = Void.class) })
    public Response postUserRatingByTapeId(@ApiParam(value = "Id of user adding his or her rating on video tape",required=true) @PathParam("userId") Long userId
,@ApiParam(value = "Id of video tape user is adding his or her rating on",required=true) @PathParam("tapeId") Long tapeId
,@ApiParam(value = "Rating of given video tape for user" ,required=true) RatingInput body
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.postUserRatingByTapeId(userId,tapeId,body,securityContext);
    }
    @PUT
    @Path("/users/{userId}/tapes/{tapeId}")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Update given user's rating for a specific tape", notes = "Update given user's rating for a specific tape", response = Void.class, tags={ "ratings", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "invalid id supplied for video tape or user", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "user rating not found on video tape", response = Void.class) })
    public Response putUserRatingByTapeId(@ApiParam(value = "Id of user updating his or her rating on video tape",required=true) @PathParam("userId") Long userId
,@ApiParam(value = "Id of video tape user is updating his or her rating on",required=true) @PathParam("tapeId") Long tapeId
,@ApiParam(value = "Rating of given video tape for user" ,required=true) RatingInput body
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.putUserRatingByTapeId(userId,tapeId,body,securityContext);
    }
}
