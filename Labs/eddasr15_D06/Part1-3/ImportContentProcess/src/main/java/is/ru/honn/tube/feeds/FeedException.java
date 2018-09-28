/*
 * @(#)FeedException.java 1.0 4 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.tube.feeds;

/**
 * Class FeedException (FeedException.java)
 * Custom exception thrown when a feed reader cannot read provided feed
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 4 Oct 2018
 */
public class FeedException extends Exception
{
  public FeedException(String message)
  {
    super(message);
  }
  public FeedException(String message, Throwable cause)
  {
    super(message, cause);
  }
}
