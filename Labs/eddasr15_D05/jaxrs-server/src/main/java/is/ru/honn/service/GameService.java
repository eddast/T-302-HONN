/*
 * @(#)GameService.java 1.0 19 Sept 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.service;

import io.swagger.model.Game;
import java.util.List;

/**
 * GameService interface (GameService.java)
 * Defines functionality for a game service class
 * (i.e. a class responsible for manipulating games data in API)
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 19 Sept 2018
 */
public interface GameService {

    /**
     * Handles GET request for all games in API
     *
     * @return the list of all games in API
     */
    List<Game> getAllGames();


    /**
     * Handles POST request for a new game to API
     *
     * @param g new game to add to API
     */
    void addGame(Game g);
    /**
     * Handles GET request for a specific game
     *
     * @param id id of game to retrieve information on
     * @return the game by id
     */
    Game getGameById(int id);
    /**
     * Handles DELETE requests for deleting a specific game from API
     *
     * @param id id of game to delete from API
     */
    void deleteGameById(int id);
    /**
     * Handles PUT request for a specific game
     *
     * @param id id of game to change in API
     * @param g new game data to replace the old game data
     */
    void updateGameById(int id, Game g);
}

