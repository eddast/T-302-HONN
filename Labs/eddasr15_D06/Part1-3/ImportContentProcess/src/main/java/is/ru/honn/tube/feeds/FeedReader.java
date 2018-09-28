/*
 * @(#)FeedReader.java 1.0 4 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.tube.feeds;

/**
 * Interface FeedReader (FeedReader.java)
 * Defines functionality necessary for a feed reader
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 4 Oct 2018
 */
public interface FeedReader
{
  /**
   * @param handler new feed handler to assign to current feed reader
   */
  void setFeedHandler(FeedHandler handler);

  /**
   * Reads feed
   *
   * @param url the url to read from
   * @throws FeedException thrown if feed could not be read
   */
  void read(String url) throws FeedException;
}
