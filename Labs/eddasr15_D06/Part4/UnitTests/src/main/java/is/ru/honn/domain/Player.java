/*
 * @(#)Player.java 1.0 4 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.domain;

/**
 * Class Player (Player.java)
 * Data structure representing a player resource in system
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 4 Oct 2018
 */
public class Player
{
    /**
     * Player id
     */
    protected int playerId;

    /**
     * Player username
     */
    protected String username;

    /**
     * Player name
     */
    protected String name;

    /**
     * Empty ctor
     */
    public Player()
    {

    }

    /**
     * Initialize a player with id, username and name
     *
     * @param playerId
     * @param username
     * @param name
     */
    public Player( int playerId, String username, String name)
    {
        this.playerId = playerId;
        this.username = username;
        this.name = name;
    }

    /**
     * @return id of the player
     */
    public int getPlayerId()
    {
        return playerId;
    }

    /**
     * @param playerId new id to assign to player
     */
    public void setPlayerId(int playerId)
    {
        this.playerId = playerId;
    }

    /**
     * @return name of the player
     */
    public String getName()
    {
        return name;
    }

    /**
     * @param name new name to assign to player
     */
    public void setName(String name)
    {
        this.name = name;
    }

    /**
     * @return username of the player
     */
    public String getUsername()
    {
        return username;
    }

    /**
     * @param username new username to assign to player
     */
    public void setUsername(String username)
    {
        this.username = username;
    }
}
