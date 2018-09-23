/*
 * @(#)FeedEntry.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.feeds;

/**
 * Class FeedEntry (FeedEntry.java)
 * Data Type for feed entries containing link and title
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 5 Sep 2018
 */
public class FeedEntry
{
    /**
     * feed title
     */
    private String title;

    /**
     * feed link
     */
    private String link;

    /**
     * Constructs data type feed entry with title and link
     *
     * @param title for feed
     * @param link to feed
     */
    public FeedEntry(String title, String link)
    {
        this.title = title;
        this.link = link;
    }

    /**
     * @return title of feed
     */
    public String getTitle()
    {
        return title;
    }

    /**
     * @return feed's link
     */
    public String getLink()
    {
        return link;
    }

    @Override
    /**
     * Defines how to write out FeedEntry in console
     */
    public String toString()
    {
        return title + '\n' + link;
    }
}
