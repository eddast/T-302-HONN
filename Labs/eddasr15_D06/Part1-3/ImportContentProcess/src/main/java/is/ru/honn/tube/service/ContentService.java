/*
 * @(#)ContentService.java 1.0 4 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.tube.service;

import java.util.Collection;

/**
 * Interface ContentService (ContentService.java)
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 4 Okt 2018
 */
public interface ContentService
{
    /**
     * Defines behavior when a content is added
     *
     * @param content the content that is to be added to system
     */
    void addContent(Content content);

    /**
     * @return all contents currently in system
     */
    Collection<Content> getContents();
}
