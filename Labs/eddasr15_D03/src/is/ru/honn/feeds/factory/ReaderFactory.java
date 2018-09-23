/*
 * @(#)ReaderFactory.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

package is.ru.honn.feeds.factory;

import is.ru.honn.feeds.FactoryException;
import is.ru.honn.feeds.FeedProperties;
import is.ru.honn.feeds.FeedReader;

/**
 * Class ReaderFactory (ReaderFactory.java)
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 5 Sep 2018
 */
public class ReaderFactory
{

    /**
     * Initiates new feed reader of type specified through properties file
     * @param source URL to feed to set up for feed reader
     * @return the feed reader specified with URL to feed set up
     */
    public static FeedReader getFeedReader(String source)
    {
        Class reader = FeedProperties.getReader();
        FeedReader feedReader = null;

        try
        {
            feedReader = (FeedReader) reader.newInstance();
            feedReader.setSource(source);
        }
        catch (InstantiationException e)
        {
            e.printStackTrace();
        }
        catch (IllegalAccessException e)
        {
            e.printStackTrace();
        }
        catch (FactoryException e)
        {
            e.printStackTrace();
        }


        return feedReader;
    }
}
