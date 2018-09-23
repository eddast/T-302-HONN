package io.swagger.api;

import io.swagger.api.*;
import io.swagger.model.*;

import org.glassfish.jersey.media.multipart.FormDataContentDisposition;

import io.swagger.model.Developer;
import io.swagger.model.Game;

import java.util.List;
import io.swagger.api.NotFoundException;

import java.io.InputStream;

import javax.ws.rs.core.Response;
import javax.ws.rs.core.SecurityContext;
import javax.validation.constraints.*;
@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-13T18:18:20.475Z")
public abstract class DevelopersApiService {
    public abstract Response addDeveloper(Developer body,SecurityContext securityContext) throws NotFoundException;
    public abstract Response assignGameByIdFromDeveloperById(Long developerId,Long gameId,SecurityContext securityContext) throws NotFoundException;
    public abstract Response assignGameByIdToDeveloperById(Long developerId,Long gameId,SecurityContext securityContext) throws NotFoundException;
    public abstract Response deleteDeveloperById(Long developerId,SecurityContext securityContext) throws NotFoundException;
    public abstract Response getDeveloperById(Long developerId,SecurityContext securityContext) throws NotFoundException;
    public abstract Response getDevelopers(SecurityContext securityContext) throws NotFoundException;
    public abstract Response getGamesByDeveloperById(Long developerId,SecurityContext securityContext) throws NotFoundException;
    public abstract Response updateDeveloperById(Long developerId,Developer body,SecurityContext securityContext) throws NotFoundException;
}
