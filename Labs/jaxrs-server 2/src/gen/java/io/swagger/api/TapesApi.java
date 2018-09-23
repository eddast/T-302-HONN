package io.swagger.api;

import io.swagger.model.*;
import io.swagger.api.TapesApiService;
import io.swagger.api.factories.TapesApiServiceFactory;

import io.swagger.annotations.ApiParam;
import io.swagger.jaxrs.*;

import io.swagger.model.Tape;
import io.swagger.model.TapeInput;

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

@Path("/tapes")


@io.swagger.annotations.Api(description = "the tapes API")
@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-16T17:01:38.449Z")
public class TapesApi  {
   private final TapesApiService delegate;

   public TapesApi(@Context ServletConfig servletContext) {
      TapesApiService delegate = null;

      if (servletContext != null) {
         String implClass = servletContext.getInitParameter("TapesApi.implementation");
         if (implClass != null && !"".equals(implClass.trim())) {
            try {
               delegate = (TapesApiService) Class.forName(implClass).newInstance();
            } catch (Exception e) {
               throw new RuntimeException(e);
            }
         } 
      }

      if (delegate == null) {
         delegate = TapesApiServiceFactory.getTapesApi();
      }

      this.delegate = delegate;
   }

    @POST
    
    @Consumes({ "application/json" })
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Add a new video tape to system", notes = "", response = Void.class, tags={ "tapes", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 201, message = "Created", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 405, message = "Invalid input", response = Void.class) })
    public Response addTape(@ApiParam(value = "Tape object to be added to system" ,required=true) TapeInput body
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.addTape(body,securityContext);
    }
    @DELETE
    @Path("/{id}")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Deletes video tape by id", notes = "Deletes video tape by id", response = Void.class, tags={ "tapes", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "invalid id supplied", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "video tape not found", response = Void.class) })
    public Response deleteTapeById(@ApiParam(value = "id of video tape to delete",required=true) @PathParam("id") Long id
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.deleteTapeById(id,securityContext);
    }
    @GET
    @Path("/{id}")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Gets specific video tape by id", notes = "", response = Tape.class, tags={ "tapes", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Tape.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "invalid id supplied", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "video tape not found", response = Void.class) })
    public Response getTape(@ApiParam(value = "id of video tape to get",required=true) @PathParam("id") Long id
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.getTape(id,securityContext);
    }
    @GET
    
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Gets list of all video tapes", notes = "", response = Tape.class, responseContainer = "List", tags={ "tapes", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Tape.class, responseContainer = "List") })
    public Response getTapes(@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.getTapes(securityContext);
    }
    @PUT
    @Path("/{id}")
    @Consumes({ "application/json" })
    
    @io.swagger.annotations.ApiOperation(value = "Update video tape by ID", notes = "", response = Void.class, tags={ "tapes", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "invalid id supplied", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "video tape not found", response = Void.class) })
    public Response updateTapeById(@ApiParam(value = "id of video tape to delete",required=true) @PathParam("id") Long id
,@ApiParam(value = "video tape object to update" ,required=true) TapeInput body
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.updateTapeById(id,body,securityContext);
    }
}
