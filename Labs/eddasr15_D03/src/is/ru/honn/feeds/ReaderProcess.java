/*
 * @(#)ReaderProcess.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

package is.ru.honn.feeds;

import is.ru.honn.feeds.factory.ReaderFactory;

/**
 * Class ReaderProcess (ReaderProcess.java)
 * Conducts all feed reading and processing from URL to some feed
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 5 Sep 2018
 */
public class ReaderProcess implements FeedHandler
{
    /**
     * Reads in feed from source URL
     */
    private FeedReader reader;

    /**
     * Decouples ReaderProcess from FeedReader, initiates a FeedReader for class
     */
    private ReaderFactory factory;

    /**
     * Gets and sets up a feed reader via factory class
     */
    public ReaderProcess()
    {
        /**  Eftirfarandi lína crashar forritið því RSS feedið er ekki til.
         *   Forritið stoppar því þegar það finnur að ekkert DOCTYPE er skilgreint á þessu URLi enda ekkert feed þar
         *   Nákvæm staðsetning þar sem forritið stoppar er í WireFeedInput klasanum og í línu 90 þar sem er reynt að
         *   byggja document með SaxBuilder klasanum út frá URLinu sem er ekki til
         */
        // reader = new RssFeedReader("http://instagram.com/tags/photooftheday/feed/recent.rss");

        // reader = new RssFeedReader("https://www.olafurandri.com/?feed=rss2");

        reader = factory.getFeedReader("https://www.olafurandri.com/?feed=rss2");

        reader.setFeedHandler(this);
    }

    /**
     * Reads feed via FeedReader specified
     */
    public void read()
    {
        reader.read();
    }

    /**
     * Prints feed entry received through feed reader
     *
     * @param entry entry to print
     */
    public void processEntry(FeedEntry entry)
    {
        System.out.println(entry);
    }
}