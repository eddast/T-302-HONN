/*
 * @(#)FeedProperties.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.feeds;

import java.io.*;
import java.util.Properties;

/**
 * Class FeedProperties (FeedProperties.java)
 * inherits Properties and returns appropriate feed reader to process feed
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 5 Sep 2018
 */
public class FeedProperties extends Properties
{
    /**
     * Type of feed reader to process feed with
     */
    private static Class reader = null;

    /**
     * Initializes new feed reader if feed reader is null, otherwise returns it
     *
     * @return which type of feed reader to use for feed processing
     */
    public static Class getReader()
    {
        if (reader == null)
        {

            Properties prop = new Properties();
            InputStream input = null;

            try
            {
                input = new FileInputStream("src/is/ru/honn/feeds/feeds.properties");

                // load the properties file
                prop.load(input);

                // get the property value and print it out
                reader = Class.forName(prop.getProperty("reader"));

            }
            catch (IOException e)
            {
                e.printStackTrace();
            }
            catch (ClassNotFoundException e)
            {
                e.printStackTrace();
            }
            if (input != null)
            {
                try
                {
                    input.close();
                }
                catch (IOException e)
                {
                    e.printStackTrace();
                }
            }
        }
        return reader;
    }
}
