package io.swagger.api;

import io.swagger.api.*;
import io.swagger.model.*;

import org.glassfish.jersey.media.multipart.FormDataContentDisposition;

import io.swagger.model.Game;

import java.util.List;
import io.swagger.api.NotFoundException;

import java.io.InputStream;

import javax.ws.rs.core.Response;
import javax.ws.rs.core.SecurityContext;
import javax.validation.constraints.*;
@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-13T18:18:20.475Z")
public abstract class GamesApiService {
    public abstract Response addGame(Game body,SecurityContext securityContext) throws NotFoundException;
    public abstract Response deleteGameById(Long gameId,SecurityContext securityContext) throws NotFoundException;
    public abstract Response getGameById(Long gameId,SecurityContext securityContext) throws NotFoundException;
    public abstract Response getGames(SecurityContext securityContext) throws NotFoundException;
    public abstract Response updateGameById(Long gameId,Game body,SecurityContext securityContext) throws NotFoundException;
}
