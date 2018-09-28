/*
 * @(#)Content.java 1.0 4 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.tube.service;

import java.util.Date;

/**
 * Class Content (Content.java)
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 4 Okt 2018
 */
public class Content
{
    private String title;
    private String link;
    private Date pubDate;
    private String description;
    private int approved;

    /**
     * Empty constructor to initialize new instance of content
     */
    public Content()
    {

    }
    /**
     * Constructor that takes all variables as a parameter and sets it accordingly
     * @param title
     * @param link
     * @param pubDate
     * @param description
     * @param approved
     */
    public Content (String title, String link, Date pubDate, String description, int approved)
    {
        this.title = title;
        this.link = link;
        this.pubDate = pubDate;
        this.description = description;
        this.approved = approved;
    }
    /**
     * @return content title
     */
    public String getTitle()
    {
        return title;
    }
    /**
     * @param title to assign to content
     */
    public void setTitle(String title)
    {
        this.title = title;
    }
    /**
     * @return link to content
     */
    public String getLink()
    {
        return link;
    }
    /**
     *
     * @param link to assign to content
     */
    public void setLink(String link)
    {
        this.link = link;
    }
    /**
     * @return publish date of content
     */
    public Date getPubDate()
    {
        return pubDate;
    }
    /**
     * @param pubDate to assign to content
     */
    public void setPubDate(Date pubDate)
    {
        this.pubDate = pubDate;
    }
    /**
     * @return description of content
     */
    public String getDescription()
    {
        return description;
    }
    /**
     * @param description to assign to content
     */
    public void setDescription(String description)
    {
        this.description = description;
    }
    /**
     * @return integer representing content approval
     */
    public int getApproved()
    {
        return approved;
    }
    /**
     * @param approved paramater to assign to content approval
     */
    public void setApproved(int approved)
    {
        this.approved = approved;
    }

    /**
     * @return String equivalent of a representation of a content
     */
    @Override
    public String toString() {
        return "title: " + title + ", URL: " + link + ", approved: " + approved + ", date published: " + pubDate + "\ndescription:\n" + description;
    }
}
