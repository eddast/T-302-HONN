/*
 * @(#)TestPlayerService.java 1.0 4 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

import is.ru.honn.domain.Player;
import is.ru.honn.service.PlayerService;
import is.ru.honn.service.ServiceException;
import junit.framework.TestCase;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

import java.util.logging.Logger;

@RunWith(SpringJUnit4ClassRunner.class)
@ContextConfiguration("classpath:app-test-stub.xml")


/**
 * Class TestPlayerService (TestPlayerService.java)
 * JUnit Test class that tests concrete player service classes
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 4 Oct 2018
 */
public class TestPlayerService extends TestCase
{
    Logger log = Logger.getLogger(TestPlayerService.class.getName());

    @Autowired
    private PlayerService service;

    @Before
    public void Setup()
    {

    }

    @Test
    public void testPlayer()
    {
        // Create two players
        Player player0 = new Player(0, "messi", "Messi");
        Player player1 = new Player(1, "ronaldo", "Ronaldo");

        // Try to add both players to system
        int pos = 0;
        try
        {
            service.addPlayer(player0);
            pos = service.addPlayer(player1);
        }
        catch (ServiceException e)
        {
            e.printStackTrace();
        }

        // Second player should get index 1 in list - i.e. position 2 in list
        assertEquals(2, pos+1);

        // Fetch player by ID - should get correct player
        Player playerNew = service.getPlayer(1);
        assertSame(playerNew, player1);

        // We should get service exception if we try to add same player twice
        try
        {
            service.addPlayer(player1);
        }
        catch (ServiceException s)
        {
            assertSame(ServiceException.class, s.getClass());
        }
    }
}
