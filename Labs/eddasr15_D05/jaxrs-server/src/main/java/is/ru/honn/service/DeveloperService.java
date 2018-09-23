/*
 * @(#)DeveloperService.java 1.0 19 Sept 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.service;

import io.swagger.model.*;
import java.util.List;

/**
 * DeveloperService interface (DeveloperService.java)
 * Defines functionality for a developer service class
 * (i.e. a class responsible for manipulating developers data in API)
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 19 Sept 2018
 */
public interface DeveloperService {

    /**
     * @param gs new game service class to set
     */
    void setGameService(GameService gs);
    /**
     * @return game service used
     */
    GameService getGameService();
    /**
     * handles GET request for all developers
     *
     * @return all developers
     */
    List<Developer> getAllDevelopers();
    /**
     * Handles POST request for new developer
     *
     * @param d developer to add to API
     */
    void addDeveloper(Developer d);
    /**
     * Handles GET request for specific developer
     *
     * @param id id of developer to retrieve information on
     * @return the developer requested for
     */
    Developer getDeveloperById(int id);
    /**
     * Handle DELETE request for specific developer
     *
     * @param id id of developer to delete from API
     */
    void deleteDeveloperById(int id);
    /**
     * Handles PUT request for specific developer
     *
     * @param id id of developer to update
     * @param d the changed developer data to replace the old developer data
     */
    void updateDeveloperById(int id, Developer d);
    /**
     * Handles GET request for all games by a specific developer
     *
     * @param developerId id of developer
     * @return list of all games from developer
     */
    List<Game> getGamesByDeveloper(int developerId);
    /**
     * Handles POST request to assign specific game to specific developer
     *
     * @param developerId id of developer to assign game to
     * @param gameId id of game to assign to developer
     */
    void setGamesByDeveloper(int developerId, int gameId);
    /**
     * Handles DELETE request to assign specific game from specific developer
     *
     * @param developerId id of developer to assign game from
     * @param gameId id of game from assign to developer
     */
    void deleteGameByDeveloper(int developerId, int gameId);
}
