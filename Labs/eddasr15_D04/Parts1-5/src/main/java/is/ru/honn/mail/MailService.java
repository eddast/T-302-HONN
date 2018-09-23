/*
 * @(#)MailService.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.mail;

/**
 * Interface MailService (MailService.java)
 * 
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 12 Sep 2018
 */
public interface MailService
{
    /**
     * @param mailServer new mail server to set
     */
    public void setMailServer(String mailServer);

    /**
     * @param message message to send
     */
    public void send(MailMessage message);
}

