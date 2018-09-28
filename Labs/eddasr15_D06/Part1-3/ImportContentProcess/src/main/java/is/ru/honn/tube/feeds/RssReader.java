/*
 * @(#)RSSReader.java 1.0 4 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.tube.feeds;

import com.sun.syndication.io.SyndFeedInput;
import com.sun.syndication.io.XmlReader;
import com.sun.syndication.feed.synd.SyndFeed;
import com.sun.syndication.feed.synd.SyndEntry;

import java.net.URL;
import java.net.MalformedURLException;
import java.util.List;
import java.util.Iterator;

import is.ru.honn.tube.service.Content;

/**
 * Class RSSReader (RSSReader.java)
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 4 Oct 2018
 */
public class RssReader extends AbstractFeedReader
{
  public RssReader()
  {

  }

  /**
   * @param handler the feed handler to assign to current feed reader
   */
  public void setFeedHandler(FeedHandler handler)
  {
    this.handler = handler;
  }

  /**
   * Reads feed
   *
   * @param source the url to read from
   * @throws FeedException thrown if feed could not be read
   */
  public void read(String source) throws FeedException
  {
    // Handler must be set for reader to function
    if (handler == null)
    {
      throw new FeedException("Handler unspecified");
    }
    URL feedUrl = null;

    // URL must be correct for reader to function
    try
    {
      feedUrl = new URL(source);
    }
    catch (MalformedURLException murlex)
    {
      throw new FeedException ("URL is not correct", murlex);
    }

    // Open the feed
    SyndFeedInput input = new SyndFeedInput();
    SyndFeed syndFeed = null;

    // URL must be readable for reader to function
    try
    {
      syndFeed = input.build(new XmlReader(feedUrl));
    }
    catch (Exception ioex)
    {
      throw new FeedException ("URL is not correct", ioex);
    }

    // Get the items and create content for each
    List list = syndFeed.getEntries();
    Iterator i  = list.iterator();
    SyndEntry ent = null;
    Content content;
    while (i.hasNext())
    {
      ent = (SyndEntry)i.next();
      content = new Content();
      content.setTitle(ent.getTitle());
      content.setLink(ent.getLink());
      content.setDescription(ent.getDescription().getValue());
      content.setPubDate(ent.getPublishedDate());
      handler.processContent(content);
    }
  }
}
