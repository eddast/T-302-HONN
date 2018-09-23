/*
 * @(#)TestMail.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.test;

// import is.ru.honn.mail.SMTPMailService;
import is.ru.honn.mail.MailFactory;
import is.ru.honn.mail.MailMessage;
import is.ru.honn.mail.MailService;
import is.ru.honn.mail.MailServiceException;

/**
 * Application TestMail (TestMail.java)
 * Tests functionality of is.ru.honn.mail
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 12 Sep 2018
 */
public class TestMail
{
    // private static SMTPMailService mailService;
    private static MailFactory mailFactory;
    private static MailService mailService;

    /**
     * @param args
     */
    public static void main (String[] args)
    {
        /*
        mailService = new SMTPMailService();
        mailService.send(
                "eddasr15@ru.is",
                "eddasr15@ru.is",
                "test mail",
                "This is a test mail sent from D4"
        );
        */

        mailFactory = new MailFactory();
        try
        {
            mailService = mailFactory.getMailService();
            /*mailService.send(
                    "eddasr15@ru.is",
                    "eddasr15@ru.is",
                    "test mail",
                    "This is a test mail sent from D4"
            );*/
            MailMessage message = new MailMessage(
                    "eddasr15@ru.is",
                    "eddasr15@ru.is",
                    "test mail",
                    "This is a test mail sent from D4"
            );
            mailService.send(message);
        }
        catch (MailServiceException ex)
        {
            ex.printStackTrace();
        }

    }
}
