/*
 * @(#)MemoryDeveloperService.java 1.0 19 Sept 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.service;

import io.swagger.model.Game;

import java.util.ArrayList;
import java.util.List;

/**
 * MemoryGameService class (MemoryGameService.java)
 * Defines functionality for a game service class
 * Responsible for manipulating games data in API)
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 19 Sept 2018
 */
public class MemoryGameService implements GameService {

    /**
     * All games stored in API
     */
    private ArrayList<Game> games = new ArrayList<Game>();


    /**
     * @return all games stored
     */
    public List<Game> getAllGames() {
        return this.games;
    }
    /**
     * @param g new game to add to API
     */
    public void addGame(Game g) {
        this.games.add(g);
    }
    /**
     * @param id id of game to retrieve information on
     * @return the game by id
     */
    public Game getGameById(int id) {
        for (Game g : this.games) {
            if (g.getId() == id) {
                return g;
            }
        }
        return null;
    }
    /**
     * @param id id of game to delete from API
     */
    public void deleteGameById(int id) {
        for (Game g : this.games) {
            if (g.getId() == id) {
                this.games.remove(g);
                break;
            }
        }
    }
    /**
     * @param id id of game to change in API
     * @param g new game data to replace the old game data
     */
    public void updateGameById(int id, Game g) {
        for (int i = 0; i < this.games.size(); i++) {
            if (this.games.get(i).getId() == id) {
                this.games.set(i, g);
            }
        }
    }
}
