/*
 * @(#)PlayerService.java 1.0 4 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.service;

import is.ru.honn.domain.Player;

/**
 * Interface PlayerService (PlayerService.java)
 * Defines actions of a player service that adds to and retrieves players from system
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 4 Oct 2018
 */
public interface PlayerService
{
    /**
     * Adds player to list of players
     *
     * @param player player to add to list
     * @return position of player in list
     * @throws ServiceException if player to be added is already in list
     */
    int addPlayer(Player player) throws ServiceException;

    /**
     * Gets player from a list of players by player id
     *
     * @param playerId id of player to retrieve from list
     * @return the player with the given id
     */
    Player getPlayer(int playerId);
}
