/*
 * @(#)ContentServiceStub.java 1.0 4 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.tube.service;

import java.util.ArrayList;
import java.util.Collection;

/**
 * Class ContentServiceStub (ContentServiceStub.java)
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 4 Okt 2018
 */
public class ContentServiceStub implements ContentService
{
    /**
     * Stores contents in system
     */
    private ArrayList<Content> _contents;


    /**
     * Initializes empty contents list
     */
    public ContentServiceStub()
    {
        _contents = new ArrayList<Content>();
    }

    /**
     * Adds content to list
     *
     * @param content the content that is to be added to system
     */
    public void addContent(Content content)
    {
        _contents.add(content);
    }

    /**
     * @return all contents that have currently been added to system
     */
    public Collection<Content> getContents()
    {
        return _contents;
    }
}
