/*
 * @(#)FeedHandler.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.feeds;

/**
 * Interface FeedHandler (FeedHandler.java)
 * Defines which functionality a Feed Handler class should have
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 5 Sep 2018
 */
public interface FeedHandler
{
    /**
     * Processes a single entry from feed read
     *
     * @param entry entry to process
     */
    public void processEntry(FeedEntry entry);
}
