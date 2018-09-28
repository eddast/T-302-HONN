/*
 * @(#)FeedHandler.java 1.0 4 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.tube.feeds;

import is.ru.honn.tube.service.Content;

/**
 * Interface FeedHandler (FeedHandler.java)
 *
 * Defines functionality of a feed handler class
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 4 Oct 2018
 */
public interface FeedHandler
{

  /**
   * Actions taken when content is processed
   *
   * @param content the content to process
   */
  public void processContent(Content content);
}
