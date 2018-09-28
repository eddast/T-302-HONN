package is.ru.honn.service;

import is.ru.honn.domain.Player;

import java.util.ArrayList;

public class PlayerServiceStub implements PlayerService
{
    private ArrayList<Player> _players;

    /**
     * Initialize new empty list of players
     */
    PlayerServiceStub()
    {
        _players = new ArrayList<Player>();
    }

    /**
     * Adds player to list of players
     *
     * @param player player to add to list
     * @return position of player in list
     * @throws ServiceException if player to be added is already in list
     */
    public int addPlayer(Player player) throws ServiceException
    {
        // Check if list already contains player
        if(_players.contains(player))
        {
            throw new ServiceException("Cannot add same player twice!");
        }
        // get index of new player in list and add to it
        int listPosition = _players.size();
        _players.add(player);

        // return player index in list
        return listPosition;
    }

    /**
     * Gets player from a list of players by player id
     *
     * @param playerId id of player to retrieve from list
     * @return the player with the given id
     */
    public Player getPlayer(int playerId)
    {
        // Search player list for player id
        for(int i = 0; i < _players.size(); i++)
        {
            if(_players.get(i).getPlayerId() == playerId)
            {
                return _players.get(i);
            }
        }

        // If no player is found matching id, return null
        return null;
    }
}
