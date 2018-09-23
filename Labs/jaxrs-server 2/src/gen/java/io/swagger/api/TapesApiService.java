package io.swagger.api;

import io.swagger.api.*;
import io.swagger.model.*;

import org.glassfish.jersey.media.multipart.FormDataContentDisposition;

import io.swagger.model.Tape;
import io.swagger.model.TapeInput;

import java.util.List;
import io.swagger.api.NotFoundException;

import java.io.InputStream;

import javax.ws.rs.core.Response;
import javax.ws.rs.core.SecurityContext;
import javax.validation.constraints.*;
@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-16T17:01:38.449Z")
public abstract class TapesApiService {
    public abstract Response addTape(TapeInput body,SecurityContext securityContext) throws NotFoundException;
    public abstract Response deleteTapeById(Long id,SecurityContext securityContext) throws NotFoundException;
    public abstract Response getTape(Long id,SecurityContext securityContext) throws NotFoundException;
    public abstract Response getTapes(SecurityContext securityContext) throws NotFoundException;
    public abstract Response updateTapeById(Long id,TapeInput body,SecurityContext securityContext) throws NotFoundException;
}
