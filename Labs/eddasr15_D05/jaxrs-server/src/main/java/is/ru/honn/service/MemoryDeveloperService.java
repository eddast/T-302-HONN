/*
 * @(#)MemoryDeveloperService.java 1.0 19 Sept 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.service;

import io.swagger.model.Developer;
import io.swagger.model.Game;
import is.ru.honn.model.GameByDeveloper;

import java.util.ArrayList;
import java.util.List;

/**
 * MemoryDeveloperService class (MemoryDeveloperService.java)
 * Defines functionality for a developer service class
 * Responsible for manipulating developer data in API)
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 19 Sept 2018
 */
public class MemoryDeveloperService implements DeveloperService {

    /**
     * Game service to use to query for games
     */
    private GameService gameService;
    /**
     * List of all developers currently stored in API
     */
    private List<Developer> developers = new ArrayList<Developer>();
    /**
     * List of all list of games by developers (one list of games per developer)
     */
    private List<GameByDeveloper> gamesByDevelopers = new ArrayList<GameByDeveloper>();


    /**
     * Empy constructor
     */
    public MemoryDeveloperService()
    {

    }
    /**
     * Constructs a developer service with specific game service
     *
     * @param gs game service instance should use to query for games
     */
    public MemoryDeveloperService(GameService gs) {
        this.gameService = gs;
    }
    /**
     * @param gs new game service class to set
     */
    public void setGameService(GameService gs) {
        this.gameService = gs;
    }
    /**
     * @return the game service currently in use
     */
    public GameService getGameService() {
        return this.gameService;
    }
    /**
     * @return all developers
     */
    public List<Developer> getAllDevelopers() {
        return this.developers;
    }
    /**
     * @param d developer to add to API
     */
    public void addDeveloper(Developer d) {
        this.developers.add(d);
    }
    /**
     * @param id id of developer to retrieve information on
     * @return the developer by id
     */
    public Developer getDeveloperById(int id) {
        for (Developer d : this.developers) {
            if (d.getId() == id) {
                return d;
            }
        }
        return null;
    }
    /**
     * @param id id of developer to delete from API
     */
    public void deleteDeveloperById(int id) {
        for (Developer d : this.developers) {
            if (d.getId() == id) {
                 this.developers.remove(d);
                 break;
            }
        }
    }
    /**
     * @param id id of developer to update
     * @param d the changed developer data to replace the old developer data
     */
    public void updateDeveloperById(int id, Developer d) {
        for (int i = 0; i < this.developers.size(); i++) {
            if (this.developers.get(i).getId() == id) {
                this.developers.set(i, d);
            }
        }
    }
    /**
     * @param developerId id of developer
     * @return list of all games by developer
     */
    public List<Game> getGamesByDeveloper(int developerId) {
        List<Game> games = new ArrayList<Game>();
        for (GameByDeveloper gameDev : this.gamesByDevelopers) {
            if (gameDev.getDeveloperId() == developerId) {
                games.add(gameService.getGameById(gameDev.getGameId()));
            }
        }

        return games;
    }
    /**
     * @param developerId id of developer to assign game to
     * @param gameId id of game to assign to developer
     */
    public void setGamesByDeveloper(int developerId, int gameId) {
        this.gamesByDevelopers.add(new GameByDeveloper(gameId, developerId));
    }
    /**
     * @param developerId id of developer to assign game from
     * @param gameId id of game from assign to developer
     */
    public void deleteGameByDeveloper(int developerId, int gameId) {
        for (GameByDeveloper gameDev : this.gamesByDevelopers) {
            if (gameDev.getDeveloperId() == developerId && gameDev.getGameId() == gameId) {
                this.gamesByDevelopers.remove(gameDev);
                break;
            }
        }
    }
}
