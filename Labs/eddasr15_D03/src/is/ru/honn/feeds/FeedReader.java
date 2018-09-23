/*
 * @(#)FeedReader.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.feeds;

/**
 * Interface FeedReader (FeedReader.java)
 * Defines which functionalities a feed reader needs to implement
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 5 Sep 2018
 */
public interface FeedReader
{
    /**
     * Reads feed
     *
     * @return true if feed reading was successful, false otherwise
     */
    public boolean read();

    /**
     * @param source new source to assign to feed reader
     */
    public void setSource(String source);

    /**
     * @param handler new feed handler to assign to feed reader
     */
    public void setFeedHandler(FeedHandler handler);
}