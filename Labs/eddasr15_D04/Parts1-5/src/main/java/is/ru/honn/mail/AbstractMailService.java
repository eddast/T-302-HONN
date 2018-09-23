/*
 * @(#)AbstractMailService.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.mail;

/**
 * Class AbstractMailService (AbstractMailService.java)
 * 
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 12 Sep 2018
 */
public abstract class AbstractMailService implements MailService
{
    
    protected String mailServer;
    // this is used by the factory to inject

    /**
     * @param msg message to send
     */
    public abstract void send(MailMessage msg);

    /**
     * Sets the mail server url to be used by the Mail Service
     * @param mailServer the mail server url
     */
    public void setMailServer(String mailServer)
    {
        this.mailServer = mailServer;
    }

    /**
     * @return the url to the mail server
     */
    public String getMailServer()
    {
        return mailServer;
    }
}
