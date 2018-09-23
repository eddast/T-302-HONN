/*
 * @(#)MailMessage.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.mail;

/**
 * Class MailMessage (MailMessage.java)
 * Data structure that implements message with appropriate values
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 12 Sep 2018
 */
public class MailMessage
{
    private String from;
    private String to;
    private String subject;
    private String body;

    /**
     * Construct empty message
     */
    public MailMessage()
    {
    }

    /**
     * Construct new message
     *
     * @param from sender
     * @param to reciever
     * @param subject message subject
     * @param body message body
     */
    public MailMessage(String from, String to, String subject,
                       String body)
    {
        this.from = from;
        this.to = to;
        this.subject = subject;
        this.body = body;
    }

    /**
     * @return sender
     */
    public String getFrom()
    {
        return from;
    }

    /**
     * @param from new message sender
     */
    public void setFrom(String from)
    {
        this.from = from;
    }

    /**
     * @return reciever
     */
    public String getTo()
    {
        return to;
    }

    /**
     * @param to new message reciever
     */
    public void setTo(String to)
    {
        this.to = to;
    }

    /**
     * @return message subject
     */
    public String getSubject()
    {
        return subject;
    }

    /**
     * @param subject new message subject
     */
    public void setSubject(String subject)
    {
        this.subject = subject;
    }

    /**
     * @return message body
     */
    public String getBody()
    {
        return body;
    }

    /**
     * @param body new message body
     */
    public void setBody(String body)
    {
        this.body = body;
    }

    /**
     * @return string representation of message
     */
    public String toString()
    {
        return "From: " + this.from +
                " To: " + this.to +
                " Subject:" + this.subject;
    }
}
