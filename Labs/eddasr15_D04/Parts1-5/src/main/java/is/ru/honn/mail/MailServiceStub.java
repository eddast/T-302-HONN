/*
 * @(#)MailServiceStub.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.mail;

import java.util.logging.Logger;

/**
 * Class MailServiceStub (MailServiceStub.java)
 * Implements mock functionality of sending mail
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 12 Sep 2018
 */
public class MailServiceStub extends AbstractMailService
{
    /**
     * Logs information in class
     */
    private Logger logger =
            Logger.getLogger(MailServiceStub.class.getName());

    /**
     * @param message message to "send" (mock functionality, only prints message)
     */
    public void send(MailMessage message)
    {
        logger.info("Sending mail from '" + message.getFrom() + "' to '" + message.getTo() +
                "' Subject: '" + message.getSubject() + "'");
        logger.info("Mail Server: " + this.getMailServer());
    }
}
