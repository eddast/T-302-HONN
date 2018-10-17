/*
 * @(#)MailGateway.java 1.0 10 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.rumdb.mail;

/**
 * Interface MailGateway (MailGateway.java)
 * Defines actions a gateway to send mail should have
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 10 Oct 2018
 */
public interface MailGateway
{
    /**
     * Sends mail message from recipient to receiver
     *
     * @param to recipient email
     * @param from receiver email
     * @param subject message subject
     * @param body message content
     */
    public void sendMessage(String to, String from, String subject, String body);
}
