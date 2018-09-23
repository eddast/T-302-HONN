package io.swagger.api;

import io.swagger.api.*;
import io.swagger.model.*;

import org.glassfish.jersey.media.multipart.FormDataContentDisposition;

import io.swagger.model.BorrowRecord;
import io.swagger.model.BorrowRecordInput;

import java.util.List;
import io.swagger.api.NotFoundException;

import java.io.InputStream;

import javax.ws.rs.core.Response;
import javax.ws.rs.core.SecurityContext;
import javax.validation.constraints.*;
@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-16T17:01:38.449Z")
public abstract class BorrowsApiService {
    public abstract Response addBorrowByBorrowerId(Long borrowerId,BorrowRecordInput body,SecurityContext securityContext) throws NotFoundException;
    public abstract Response deleteBorrow(Long borrowerId,Long tapeId,SecurityContext securityContext) throws NotFoundException;
    public abstract Response getBorrows(SecurityContext securityContext) throws NotFoundException;
    public abstract Response getBorrowsByBorrowerId(Long borrowerId,SecurityContext securityContext) throws NotFoundException;
    public abstract Response updateBorrowRecord(Long borrowerId,Long tapeId,BorrowRecordInput body,SecurityContext securityContext) throws NotFoundException;
}
