/*
 * @(#)DevelopersApiServiceImpl.java 1.0 19 Sept 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package io.swagger.api.impl;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import io.swagger.api.*;
import io.swagger.model.Developer;
import io.swagger.model.Game;
import java.util.List;
import io.swagger.api.NotFoundException;
import is.ru.honn.service.DeveloperService;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.SecurityContext;

/**
 * DevelopersApiServiceImpl class (DevelopersApiServiceImpl.java)
 * Rexieves and sends HTTP requests for API
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 19 Sept 2018
 */
@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-13T18:18:20.475Z")
public class DevelopersApiServiceImpl extends DevelopersApiService {

    /**
     * Developer service used to manipulate developers in API
     */
    private DeveloperService developerService;


    /**
     * @param ds developer service to use
     */
    public DevelopersApiServiceImpl(DeveloperService ds) {
        super();
        this.developerService = ds;
    }
    /**
     * Adds developer to API via developer service
     *
     * @param body body of request should contain developer object
     * @param securityContext
     * @return API response to request
     */
    @Override
    public Response addDeveloper(Developer body, SecurityContext securityContext) {
        if (!validDeveloper(body)) {
            return Response.status(405).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "All Developer fields are required")).build();
        }

        this.developerService.addDeveloper(body);

        return Response.ok().entity(new ApiResponseMessage(ApiResponseMessage.OK, "Developer added!")).build();
    }
    /**
     * Assigns game from developer in API via developer service
     *
     * @param developerId id of developer to assign game from
     * @param gameId id of game to assign from developer
     * @param securityContext
     * @return API response to request
     */
    @Override
    public Response assignGameByIdFromDeveloperById(Long developerId, Long gameId, SecurityContext securityContext) {
        if (developerId == null || gameId == null || developerId < 0 || gameId < 0) {
            return Response.status(400).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "developerId and/or gameId are required")).build();
        }

        Developer d = developerService.getDeveloperById(developerId.intValue());
        if (d == null) {
            return Response.status(404).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "developer not found")).build();
        }

        developerService.deleteGameByDeveloper(developerId.intValue(), gameId.intValue());
        return Response.ok().entity(new ApiResponseMessage(ApiResponseMessage.OK, "Game removed from developer")).build();
    }
    /**
     * Assigns game to developer in API via developer service
     *
     * @param developerId id of developer to assign game to
     * @param gameId id of game to assign to developer
     * @param securityContext
     * @return API response to request
     */
    @Override
    public Response assignGameByIdToDeveloperById(Long developerId, Long gameId, SecurityContext securityContext) {
        if (developerId == null || gameId == null || developerId < 0 || gameId < 0) {
            return Response.status(400).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "developerId and/or gameId are required")).build();
        }

        Developer d = developerService.getDeveloperById(developerId.intValue());
        if (d == null) {
            return Response.status(404).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "developer not found")).build();
        }

        Game g = developerService.getGameService().getGameById(gameId.intValue());
        if (g == null) {
            return Response.status(404).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "game not found")).build();

        }

        developerService.setGamesByDeveloper(developerId.intValue(), gameId.intValue());
        return Response.ok().entity(new ApiResponseMessage(ApiResponseMessage.OK, "Game set by developer")).build();
    }
    /**
     * Deletes developer from API via developer service
     *
     * @param developerId id of developer to delete
     * @param securityContext
     * @return API response to request
     */
    @Override
    public Response deleteDeveloperById(Long developerId, SecurityContext securityContext) {
        if (developerId == null || developerId < 0) {
            return Response.status(400).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "developerId is required")).build();
        }
        Developer d = developerService.getDeveloperById(developerId.intValue());
        if (d != null) {
            developerService.deleteDeveloperById(developerId.intValue());
            return Response.ok().entity(new ApiResponseMessage(ApiResponseMessage.OK, "Developer removed!")).build();
        }
        return Response.status(404).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "developer with id not found!")).build();

    }
    /**
     * Gets a specified developer by id via developer service
     *
     * @param developerId id of developer to get
     * @param securityContext
     * @return API response to request
     */
    @Override
    public Response getDeveloperById(Long developerId, SecurityContext securityContext) throws NotFoundException {
        if (developerId == null || developerId < 0) {
            return Response.status(400).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "developerId is required")).build();
        }

        Developer d = developerService.getDeveloperById(developerId.intValue());

        if (d != null) {
            ObjectMapper mapper = new ObjectMapper();
            String responseMessage = null;
            try {
                responseMessage = mapper.writeValueAsString(d);
                return Response.ok().entity(new ApiResponseMessage(ApiResponseMessage.OK, responseMessage)).build();
            } catch (JsonProcessingException e) {
                e.printStackTrace();
            }

        }

        return Response.status(404).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "developer with id not found!")).build();

    }
    /**
     * Gets list of developers from API via developer service
     *
     * @param securityContext
     * @return API response to request
     */
    @Override
    public Response getDevelopers(SecurityContext securityContext) {
        List<Developer> developers = developerService.getAllDevelopers();

        ObjectMapper mapper = new ObjectMapper();
        String responseMessage = null;
        try {
            responseMessage = mapper.writeValueAsString(developers);
            return Response.ok().entity(new ApiResponseMessage(ApiResponseMessage.OK, responseMessage)).build();
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }

        return Response.ok().entity(new ApiResponseMessage(ApiResponseMessage.OK, responseMessage)).build();
    }
    /**
     * Gets games assigned to specified developer in API via developer service
     *
     * @param developerId id of developer to get games assigned to
     * @param securityContext
     * @return API response to request
     */
    @Override
    public Response getGamesByDeveloperById(Long developerId, SecurityContext securityContext) {
        if (developerId == null || developerId < 0) {
            return Response.status(400).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "developerId is required")).build();
        }

        Developer d = developerService.getDeveloperById(developerId.intValue());
        if (d == null) {
            return Response.status(404).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "developer not found")).build();
        }

        List<Game> games = developerService.getGamesByDeveloper(developerId.intValue());

        ObjectMapper mapper = new ObjectMapper();
        String responseMessage = null;
        try {
            responseMessage = mapper.writeValueAsString(games);
            return Response.ok().entity(new ApiResponseMessage(ApiResponseMessage.OK, responseMessage)).build();
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }

        return Response.ok().entity(new ApiResponseMessage(ApiResponseMessage.OK, "magic!")).build();
    }
    /**
     * Updates developer in API via developer service
     *
     * @param developerId id of developer to update
     * @param body request body, should contain developer object
     * @param securityContext
     * @return API response to request
     */
    @Override
    public Response updateDeveloperById(Long developerId, Developer body, SecurityContext securityContext) {
        if (developerId == null || developerId < 0) {
            return Response.status(400).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "developerId is required")).build();
        }

        Developer d = developerService.getDeveloperById(developerId.intValue());
        if (d == null) {
            return Response.status(404).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "developer not found")).build();
        }

        developerService.updateDeveloperById(developerId.intValue(), body);
        return Response.ok().entity(new ApiResponseMessage(ApiResponseMessage.OK, "Developer updated!")).build();
    }
    /**
     * @param d developer object to validate
     * @return true if developer is valid i.e. in API, false otherwise
     */
    private boolean validDeveloper(Developer d) {
        return ( d.getName() != null &&
                 d.getId() != null &&
                 d.getFounded() != null);
    }
}
