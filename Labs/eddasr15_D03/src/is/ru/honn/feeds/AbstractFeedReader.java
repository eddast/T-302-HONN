/*
 * @(#)AbstractFeedReader.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

package is.ru.honn.feeds;

/**
 * Class AbstractFeedReader (AbstractFeedReader.java)
 * Defines functionality to be present in Feed Reader classes
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 5 Sep 2018
 */
public abstract class AbstractFeedReader implements FeedReader
{
    /**
     * Source URL to feed
     */
    protected String source;

    /**
     * FeedHandler used to process feed
     */
    protected FeedHandler feedHandler;

    /**
     * Reads feed from source URL to some feed
     *
     * @return true if feed was successfully read, false otherwise
     */
    public abstract boolean read();

    /**
     * @param source source URL to feed to assign to the current instance of a feed reader
     */
    public void setSource(String source)
    {
        this.source = source;
    }

    /**
     * @param feedHandler the FeedHandler to assign to the current instance of a feed reader
     */
    public void setFeedHandler(FeedHandler feedHandler)
    {
        this.feedHandler = feedHandler;
    }
}
