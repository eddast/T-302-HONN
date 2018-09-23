/*
 * @(#)ServiceFactory.java 1.0 19 Sept 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.factory;

import is.ru.honn.service.DeveloperService;
import is.ru.honn.service.GameService;
import is.ru.honn.service.MemoryDeveloperService;
import is.ru.honn.service.MemoryGameService;

/**
 * ServiceFactory class (ServiceFactory.java)
 * Initializes services if needed to use for API
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 19 Sept 2018
 */
public class ServiceFactory
{


    /**
     * service model that manipulates games stored in API
     */
    private static GameService gs;
    /**
     * service model that manipulates developers stored in API
     */
    private static DeveloperService ds;



    /**
     * @return new instance game service if one does not already exist
     */
    public static GameService getGameService()
    {
        if(gs == null)
        {
            gs = new MemoryGameService();
        }
        return gs;
    }
    /**
     * @return new instance developer service if one does not already exist
     */
    public static DeveloperService getDeveloperService()
    {
        if(ds == null){
            ds = new MemoryDeveloperService();
            ds.setGameService(getGameService());
        }
        return ds;
    }
}
