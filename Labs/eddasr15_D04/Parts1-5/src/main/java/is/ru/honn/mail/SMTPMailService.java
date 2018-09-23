/*
 * @(#)SMTPMailService.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.mail;

import javax.mail.internet.MimeMessage;
import javax.mail.internet.InternetAddress;
import javax.mail.Session;
import javax.mail.Transport;
import javax.mail.Message;
import java.util.Date;
import java.util.Properties;
import java.util.logging.Logger;

/**
 * Class SMTPMailService (SMTPMailService.java)
 * Sends message via SMTP protocol
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 12 Sep 2018
 */
public class SMTPMailService extends AbstractMailService
{
    /**
     * Logs errors and information for class
     */
    private Logger logger = Logger.getLogger(SMTPMailService.class.getName());

    /**
     * @param message message to send
     */
    public void send(MailMessage message)
    {
        // Testing illegal mail server
        // String smtpServer = "aosidfjosadf";
        // String smtpServer = "smtp.ru.is";
        try
        {
            Properties props = System.getProperties();
            props.put("mail.smtp.host", getMailServer());
            Session session = Session.getDefaultInstance(props, null);
            Message msg = new MimeMessage(session);
            msg.setFrom(new InternetAddress(message.getFrom()));
            msg.setRecipients(Message.RecipientType.TO,
                    InternetAddress.parse(message.getTo(), false));
            msg.setSubject(message.getSubject());
            msg.setText(message.getBody());
            msg.setSentDate(new Date());
            Transport.send(msg);
        }
        catch (Exception ex)
        {
            String msg = "MailService: Sending mail failed: " + ex.getMessage();
            logger.severe(msg);
            throw new MailServiceException(msg, ex);
        }
    }
}

