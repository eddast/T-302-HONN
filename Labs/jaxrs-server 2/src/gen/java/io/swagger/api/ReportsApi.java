package io.swagger.api;

import io.swagger.model.*;
import io.swagger.api.ReportsApiService;
import io.swagger.api.factories.ReportsApiServiceFactory;

import io.swagger.annotations.ApiParam;
import io.swagger.jaxrs.*;

import io.swagger.model.BorrowedTapeDetails;
import io.swagger.model.Borrower;

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

@Path("/reports")


@io.swagger.annotations.Api(description = "the reports API")
@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-16T17:01:38.449Z")
public class ReportsApi  {
   private final ReportsApiService delegate;

   public ReportsApi(@Context ServletConfig servletContext) {
      ReportsApiService delegate = null;

      if (servletContext != null) {
         String implClass = servletContext.getInitParameter("ReportsApi.implementation");
         if (implClass != null && !"".equals(implClass.trim())) {
            try {
               delegate = (ReportsApiService) Class.forName(implClass).newInstance();
            } catch (Exception e) {
               throw new RuntimeException(e);
            }
         } 
      }

      if (delegate == null) {
         delegate = ReportsApiServiceFactory.getReportsApi();
      }

      this.delegate = delegate;
   }

    @GET
    @Path("/borrowedTapes")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Gets list of all borrowed video tapes in system and who has them", notes = "Gets list of all borrowed video tapes in system and who has them", response = BorrowedTapeDetails.class, responseContainer = "List", tags={ "reports", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = BorrowedTapeDetails.class, responseContainer = "List") })
    public Response getBorrowedTapesReport(@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.getBorrowedTapesReport(securityContext);
    }
    @GET
    @Path("/borrowers")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Gets list of all friends that have borrowed tapes and which tapes", notes = "Gets list of all friends that have borrowed tapes and which tapes", response = Borrower.class, responseContainer = "List", tags={ "reports", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Borrower.class, responseContainer = "List") })
    public Response getBorrowersReport(@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.getBorrowersReport(securityContext);
    }
    @GET
    @Path("/lateBorrowers")
    
    @Produces({ "application/json" })
    @io.swagger.annotations.ApiOperation(value = "Gets list of all friends that have been borrowing tapes from over a month ago and which tapes they've borrowed", notes = "Gets list of all friends that have been borrowing tapes from over a month ago and which tapes they've borrowed", response = Borrower.class, responseContainer = "List", tags={ "reports", })
    @io.swagger.annotations.ApiResponses(value = { 
        @io.swagger.annotations.ApiResponse(code = 200, message = "successful operation", response = Borrower.class, responseContainer = "List") })
    public Response getLateBorrowersReport(@Context SecurityContext securityContext)
    throws NotFoundException {
        return delegate.getLateBorrowersReport(securityContext);
    }
}
