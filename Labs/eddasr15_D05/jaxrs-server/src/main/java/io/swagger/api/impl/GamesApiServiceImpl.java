/*
 * @(#)GamesApiServiceImpl.java 1.0 19 Sept 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package io.swagger.api.impl;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import io.swagger.api.*;
import io.swagger.model.Game;
import java.net.URI;
import java.net.URISyntaxException;
import java.util.List;
import is.ru.honn.service.GameService;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.SecurityContext;

/**
 * GamesApiServiceImpl class (GamesApiServiceImpl.java)
 * Rexieves and sends HTTP requests for API
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 19 Sept 2018
 */
@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-13T18:18:20.475Z")
public class GamesApiServiceImpl extends GamesApiService
{
    /**
     * Game service used to manipulate games in API
     */
    private GameService gameService;


    /**
     * @param gs game service to use
     */
    public GamesApiServiceImpl(GameService gs)
    {
        super();
        this.gameService = gs;
    }
    /**
     * Adds game to API via game service
     *
     * @param body request body, should be a game object
     * @param securityContext
     * @return API http reponse to the  request
     */
    @Override
    public Response addGame(Game body, SecurityContext securityContext)
    {
        if (validGame(body))
        {
            this.gameService.addGame(body);
            URI uri = null;
            try
            {
                uri = new URI("/games/" + body.getId());
            }
            catch (URISyntaxException e)
            {
                e.printStackTrace();
            }
            if (uri != null)
            {
                return Response.created(uri).entity(new ApiResponseMessage(ApiResponseMessage.OK, "Game has been added!")).build();
            }
        }
        return Response.status(405).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "Please fill out all required properties")).build();
    }
    /**
     * Deletes game by id from API via game service
     *
     * @param gameId id of game to delete
     * @param securityContext
     * @return API http reponse to the  request
     */
    @Override
    public Response deleteGameById(Long gameId, SecurityContext securityContext)
    {
        if (gameId == null || gameId < 0)
        {
            return Response.status(400).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "gameId is required")).build();
        }
        Game g  = gameService.getGameById(gameId.intValue());
        if (g == null)
        {
            return Response.status(404).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "Game not found")).build();
        }

        gameService.deleteGameById(gameId.intValue());
        return Response.ok().entity(new ApiResponseMessage(ApiResponseMessage.OK, "Game has been deleted")).build();
    }
    /**
     * Retrieves game by id via game service
     *
     * @param gameId id of game to retrieve
     * @param securityContext
     * @return API HTTP reponse to request
     */
    @Override
    public Response getGameById(Long gameId, SecurityContext securityContext)
    {
        if (gameId == null || gameId < 0)
        {
            return Response.status(400).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "gameId is required")).build();
        }

        Game g = gameService.getGameById(gameId.intValue());
        if (g != null)
        {
            ObjectMapper mapper = new ObjectMapper();
            String responseMessage = null;
            try
            {
                responseMessage = mapper.writeValueAsString(g);
                return Response.ok().entity(new ApiResponseMessage(ApiResponseMessage.OK, responseMessage)).build();
            }
            catch (JsonProcessingException e)
            {
                e.printStackTrace();
            }
        }
        return Response.status(404).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "Game not found")).build();
    }
    /**
     * Gets all games from API via game service
     *
     * @param securityContext
     * @return API HTTP response to request
     */
    @Override
    public Response getGames(SecurityContext securityContext)
    {
        List<Game> games = gameService.getAllGames();
        ObjectMapper mapper = new ObjectMapper();
        String responseMessage = null;
        try
        {
            responseMessage = mapper.writeValueAsString(games);
            return Response.ok().entity(new ApiResponseMessage(ApiResponseMessage.OK, responseMessage)).build();
        }
        catch (JsonProcessingException e)
        {
            e.printStackTrace();
        }

        return Response.status(400).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "Something went wrong, please try again later!")).build();
    }
    /**
     * Updates specific game in API via game service
     *
     * @param gameId id of game to update
     * @param body http body should contain updated game
     * @param securityContext
     * @return API HTTP response to request
     */
    @Override
    public Response updateGameById(Long gameId, Game body, SecurityContext securityContext)
    {
        if (gameId == null || gameId < 0)
        {
            return Response.status(400).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "gameId is required")).build();
        }
        Game g  = gameService.getGameById(gameId.intValue());
        if (g == null)
        {
            return Response.status(404).entity(new ApiResponseMessage(ApiResponseMessage.ERROR, "Game not found")).build();
        }

        gameService.updateGameById(gameId.intValue(), body);
        return Response.ok().entity(new ApiResponseMessage(ApiResponseMessage.OK, "Game has been updated!")).build();
    }
    /**
     * @param g game object to validate
     * @return true if game is valid i.e. in API, false otherwise
     */
    private boolean validGame(Game g)
    {
        return ( g.getGenre() != null &&
                 g.getId() != null &&
                 g.getName() != null &&
                 g.getReleaseDate() != null );
    }
}
