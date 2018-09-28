/*
 * @(#)ImportContentProcess.java 1.0 4 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.tube.process;

import is.ru.honn.tube.feeds.FeedException;
import is.ru.honn.tube.feeds.FeedHandler;
import is.ru.honn.tube.feeds.RssReader;
import is.ru.honn.tube.service.Content;
import is.ru.honn.tube.service.ContentService;
import is.ruframework.process.RuAbstractProcess;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.FileSystemXmlApplicationContext;
import org.springframework.context.support.ResourceBundleMessageSource;

import java.util.Collection;
import java.util.Locale;

/**
 * Class ImportContentProcess (ImportContentProcess.java)
 * Defines the behaviour before, after and during for RuProcess that reads from feed
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 4 Okt 2018
 */
public class ImportContentProcess extends RuAbstractProcess implements FeedHandler {

    protected ContentService contentService;
    RssReader reader;
    ResourceBundleMessageSource messageBundler;
    // Locale locale = new Locale("IS");
    Locale locale = new Locale("EN");

    /**
     * Handles initialization before the process is started
     */
    public void beforeProcess()
    {
        ApplicationContext applicationContext = new FileSystemXmlApplicationContext("classpath:app.xml");
        messageBundler = new ResourceBundleMessageSource();
        messageBundler.setBasename("messages");
        messageBundler.setDefaultEncoding("UTF-8");

        contentService = (ContentService) applicationContext.getBean("contentService");
        reader = (RssReader) applicationContext.getBean("feedReader");
        reader.setFeedHandler(this);
        System.out.println(messageBundler.getMessage("processbefore", null, "Default", locale));
    }

    /**
     * Handles logic required to start the reader process
     */
    public void startProcess()
    {
        System.out.println(messageBundler.getMessage("processstart", null, "Default", locale));
        try
        {
            reader.read("https://mbl.is/feeds/togt/");
        }
        catch (FeedException e)
        {
            System.out.println(messageBundler.getMessage("processreaderror", null, "Default", locale));
        }
    }

    /**
     * Handles output after the process has finished reading
     */
    public void afterProcess()
    {
        System.out.println(messageBundler.getMessage("processstartdone", null, "Default", locale));
        Collection<Content> contents = contentService.getContents();
        for(Content content: contents)
        {
            System.out.println(content);
        }
    }

    /**
     * processes the content read from the feed and adds it to data
     * @param content that was read
     */
    public void processContent(Content content)
    {
        this.contentService.addContent(content);
    }
}
