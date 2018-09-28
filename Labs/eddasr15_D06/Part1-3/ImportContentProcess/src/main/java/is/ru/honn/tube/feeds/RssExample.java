/*
 * @(#)RSSExample.java 1.0 4 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.tube.feeds;

import java.net.URL;
import java.net.MalformedURLException;
import java.util.List;
import java.util.Iterator;

import com.sun.syndication.io.XmlReader;
import com.sun.syndication.io.SyndFeedInput;
import com.sun.syndication.feed.synd.SyndFeed;
import com.sun.syndication.feed.synd.SyndEntry;

/**
 * Application RSSExample (RSSExample.java)
 * Reads some XML feed content from URL using package SyndFeed
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 4 Oct 2018
 */
public class RssExample
{
    /**
     * Runs new RSS example
     *
     * @param args regards the first command line argument as specification which url to read
     */
    public static void main(String[] args)
    {
        new RssExample(args[0]);
    }

    /**
     *
     * @param importURL the URL for feed reader to read from
     */
    public RssExample(String importURL)
    {
        URL feedUrl = null;

        // URL must be a valid URL for reader to function
        try
        {
            feedUrl = new URL(importURL);
        } catch (MalformedURLException murlex)
        {
            System.out.println("Error reading URLs");
        }
        SyndFeedInput input = new SyndFeedInput();
        SyndFeed syndFeed = null;

        // URL must readable as xml feed for reader to function
        try
        {
            syndFeed = input.build(new XmlReader(feedUrl));
        } catch (Exception ioex)
        {
            System.out.println("Error reading URL");
        }

        // iterate through and output entries of feed
        List list = syndFeed.getEntries();
        Iterator i = list.iterator();
        SyndEntry ent = null;
        while (i.hasNext())
        {
            ent = (SyndEntry) i.next();
            System.out.println(ent.getTitle());
            System.out.println(ent.getDescription().getValue());
        }
    }
}