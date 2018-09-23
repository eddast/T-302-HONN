package io.swagger.api;

import io.swagger.model.*;
import io.swagger.api.RecommendationsApiService;
import io.swagger.api.factories.RecommendationsApiServiceFactory;

import io.swagger.annotations.ApiParam;
import io.swagger.jaxrs.*;

import io.swagger.model.Tape;

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

@Path("/recommendations")


@io.swagger.annotations.Api(description = "the recommendations API")
@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-16T17:01:38.449Z")
public class RecommendationsApi  {
   private final RecommendationsApiService delegate;

   public RecommendationsApi(@Context ServletConfig servletContext) {
      RecommendationsApiService delegate = null;

      if (servletContext != null) {
         String implClass = servletContext.getInitParameter("RecommendationsApi.implementation");
         if (implClass != null && !"".equals(implClass.trim())) {
            try {
               delegate = (RecommendationsApiService) Class.forName(implClass).newInstance();
            } catch (Exception e) {
               throw new RuntimeException(e);
            }
         } 
      }

      if (delegate == null) {
         delegate = RecommendationsApiServiceFactory.getRecommendationsApi();
      }

      this.delegate = delegate;
   }

    @GET
    @Path("/{userId}")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Gets real-time recommendation on a video tape for a given user", notes = "Gets real-time recommendation on a video tape for a given user", response = Tape.class, responseContainer = "List", tags={ "recommendations", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Tape.class, responseContainer = "List"),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "invalid id supplied for user", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "user not found with id", response = Void.class) })
    public Response getUserRecommendation(@ApiParam(value = "Id of user requesting recommendation on a video tape",required=true) @PathParam("userId") Long userId
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.getUserRecommendation(userId,securityContext);
    }
}
