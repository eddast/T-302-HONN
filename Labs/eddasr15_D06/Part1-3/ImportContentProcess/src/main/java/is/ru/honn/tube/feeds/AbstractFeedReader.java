/*
 * @(#)AbstractFeedReader.java 1.0 4 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.tube.feeds;

/**
 * Class AbstractFeedReader (AbstractFeedReader.java)
 * Defines and partly implements functionality of a feed reader class
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 4 Oct 2018
 */
public abstract class AbstractFeedReader implements FeedReader
{
  /**
   * Feed handler is used for the feed reader
   */
  protected FeedHandler handler;

  /**
   * @param handler new feed handler to assign to current feed reader
   */
  public void setFeedHandler(FeedHandler handler)
  {
    this.handler = handler;
  }

  /**
   * Reads feed
   *
   * @param url the url to read from
   * @throws FeedException thrown if feed could not be read
   */
  public abstract void read(String url) throws FeedException;
}
