package io.swagger.api;

import io.swagger.model.*;
import io.swagger.api.BorrowsApiService;
import io.swagger.api.factories.BorrowsApiServiceFactory;

import io.swagger.annotations.ApiParam;
import io.swagger.jaxrs.*;

import io.swagger.model.BorrowRecord;
import io.swagger.model.BorrowRecordInput;

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

@Path("/borrows")


@io.swagger.annotations.Api(description = "the borrows API")
@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-16T17:01:38.449Z")
public class BorrowsApi  {
   private final BorrowsApiService delegate;

   public BorrowsApi(@Context ServletConfig servletContext) {
      BorrowsApiService delegate = null;

      if (servletContext != null) {
         String implClass = servletContext.getInitParameter("BorrowsApi.implementation");
         if (implClass != null && !"".equals(implClass.trim())) {
            try {
               delegate = (BorrowsApiService) Class.forName(implClass).newInstance();
            } catch (Exception e) {
               throw new RuntimeException(e);
            }
         } 
      }

      if (delegate == null) {
         delegate = BorrowsApiServiceFactory.getBorrowsApi();
      }

      this.delegate = delegate;
   }

    @POST
    @Path("/{borrowerId}")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Add a new borrow record for a given tape to borrower by borrower id to system", notes = "Add a new borrow record for a given tape to borrower by borrower id to system", response = Void.class, tags={ "borrows", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "invalid id supplied for borrower", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "borrower not found by id", response = Void.class) })
    public Response addBorrowByBorrowerId(@ApiParam(value = "Id of borrower to delete borrow record of for a certain tape",required=true) @PathParam("borrowerId") Long borrowerId
,@ApiParam(value = "New borrow record to register for borrower" ,required=true) BorrowRecordInput body
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.addBorrowByBorrowerId(borrowerId,body,securityContext);
    }
    @DELETE
    @Path("/{borrowerId}/tapes/{tapeId}")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Deletes borrow record for a borrower by id for a tape by id", notes = "Deletes borrow record for a borrower by id for a tape by id", response = Void.class, tags={ "borrows", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "invalid id supplied for borrower or video tape", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "borrower or video tape not found by id", response = Void.class) })
    public Response deleteBorrow(@ApiParam(value = "Id of borrower to delete borrow record of for a certain tape",required=true) @PathParam("borrowerId") Long borrowerId
,@ApiParam(value = "Id of video tape to delete borrow record of for a certain borrower",required=true) @PathParam("tapeId") Long tapeId
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.deleteBorrow(borrowerId,tapeId,securityContext);
    }
    @GET
    
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Gets list of all borrow records of tapes borrowed to friends", notes = "", response = BorrowRecord.class, responseContainer = "List", tags={ "borrows", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = BorrowRecord.class, responseContainer = "List") })
    public Response getBorrows(@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.getBorrows(securityContext);
    }
    @GET
    @Path("/{borrowerId}")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Gets all borrow records for specific borrower by id", notes = "Gets all borrow records for specific borrower by id", response = BorrowRecord.class, responseContainer = "List", tags={ "borrows", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = BorrowRecord.class, responseContainer = "List"),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "invalid id supplied for borrower", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "borrower not found by id", response = Void.class) })
    public Response getBorrowsByBorrowerId(@ApiParam(value = "Id of borrower that has borrowed video tapes",required=true) @PathParam("borrowerId") Long borrowerId
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.getBorrowsByBorrowerId(borrowerId,securityContext);
    }
    @PUT
    @Path("/{borrowerId}/tapes/{tapeId}")
    @Consumes({ "application/json" })
    
    @io.swagger.annotations.ApiOperation(value = "Update borrow record for a video tape borrowed", notes = "", response = Void.class, tags={ "borrows", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 400, message = "invalid id supplied for borrower or video tape", response = Void.class),
        
        @io.swagger.annotations.ApiResponse(code = 404, message = "borrower or video tape not found", response = Void.class) })
    public Response updateBorrowRecord(@ApiParam(value = "Id of borrower to update a borrow record for",required=true) @PathParam("borrowerId") Long borrowerId
,@ApiParam(value = "Id of video tape to update a borrow record for",required=true) @PathParam("tapeId") Long tapeId
,@ApiParam(value = "Borrow record to update for borrower" ,required=true) BorrowRecordInput body
,@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.updateBorrowRecord(borrowerId,tapeId,body,securityContext);
    }
}
