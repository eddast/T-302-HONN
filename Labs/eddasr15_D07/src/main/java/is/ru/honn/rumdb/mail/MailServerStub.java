/*
 * @(#)MailServerStub.java 1.0 10 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.rumdb.mail;

/**
 * Class MailServerStub (MailServerStub.java)
 * Does nothing but mock a mail gateway but only writes out parameters
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 10 Oct 2018
 */
public class MailServerStub implements MailGateway
{

    /**
     * Sends mail message from recipient to receiver
     *
     * @param to      recipient email
     * @param from    receiver email
     * @param subject message subject
     * @param body    message content
     */
    public void sendMessage(String to, String from, String subject, String body)
    {
        System.out.println("\n-----------------MAIL---------------------");
        System.out.println("recipient: " + to + ", receiver: " + from);
        System.out.println("subject: " + subject);
        System.out.println("message: " + body);
        System.out.println("------------------------------------------");
    }
}
