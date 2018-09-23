/*
 * @(#)GameByDeveloper.java 1.0 19 Sept 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.model;

/**
 * GameByDeveloper class (GameByDeveloper.java)
 * Data structure representing relation between developer and game
 * (i.e. which games are developed by which developer)
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 19 Sept 2018
 */
public class GameByDeveloper
{

    /**
     * id of game
     */
    private int gameId;
    /**
     * id of developer
     */
    private int developerId;


    /**
     * construct relation between game and developer,
     * i.e. assign game to developer
     *
     * @param gameId id of game to assign to developer
     * @param developerId id of developer to assign game to
     */
    public GameByDeveloper(int gameId, int developerId)
    {
        this.gameId = gameId;
        this.developerId = developerId;
    }
    /**
     * @return id of game in relation
     */
    public int getGameId()
    {
        return this.gameId;
    }
    /**
     * @return id of developer in relation
     */
    public int getDeveloperId()
    {
        return this.developerId;
    }
}
