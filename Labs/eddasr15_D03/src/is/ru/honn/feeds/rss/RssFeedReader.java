/*
 * @(#)RssFeedReader.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

package is.ru.honn.feeds.rss;

import com.sun.syndication.feed.synd.SyndEntry;
import com.sun.syndication.feed.synd.SyndFeed;
import com.sun.syndication.io.FeedException;
import com.sun.syndication.io.SyndFeedInput;
import com.sun.syndication.io.XmlReader;
import is.ru.honn.feeds.AbstractFeedReader;
import is.ru.honn.feeds.FeedEntry;
import is.ru.honn.feeds.FeedHandler;

import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.Iterator;
import java.util.List;

/**
 * Class RssFeedReader (RssFeedReader.java)
 * Defines actions of a feed reader
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 5 Sep 2018
 */
public class RssFeedReader extends AbstractFeedReader
{

    /**
     * Constructs feed reader
     * Needed because it enables calling the newInstance() method on class
     */
    public RssFeedReader()
    {
        // Athugið að það er mikilvægt að hafa þennan smið
        // þó hann geri ekkert.
        // Ef honum er sleppt þá koma ekki þýðingavillur en
        // seinna í verkefninu notum við “reflection” til að
        // búa þennan klasa til og þá verður að vera til
        // færibreytulaus smiður.
    }

    /**
     * Constructs feed reader
     *
     * @param source URL to construct feed reader from
     */
    public RssFeedReader(String source)
    {
        this.source = source;
    }

    /**
     * @return the feed reader's source URL
     */
    public String getSource()
    {
        return source;
    }

    /**
     * @return the feed handler used by feed reader
     */
    public FeedHandler getFeedHandler()
    {
        return feedHandler;
    }

    /**
     * Reads feed from source URL specified and calls the process entry
     * function of the specified FeedHandler
     *
     * @return false if reader is unable to reed feed, true otherwise
     */
    public boolean read()
    {
        URL feedUrl = null;

        try
        {
            feedUrl = new URL(source);
        }
        catch (MalformedURLException murlex)
        {
            System.out.println("URL is not correct (Malformed)");
        }

        SyndFeedInput input = new SyndFeedInput();
        SyndFeed syndFeed = null;

        try
        {
            syndFeed = input.build(new XmlReader(feedUrl));
        }
        catch (FeedException fex)
        {
            fex.printStackTrace();
            System.out.println("URL is not correct");
            return false;
        }
        catch (IOException ioex)
        {
            System.out.println("URL is not correct");
            return false;
        }

        List list = syndFeed.getEntries();
        Iterator i = list.iterator();
        SyndEntry ent = null;

        while (i.hasNext())
        {
            ent = (SyndEntry) i.next();
            feedHandler.processEntry(new FeedEntry(ent.getTitle(), ent.getLink()));
        }

        return true;
    }
}