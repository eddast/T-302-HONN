/*
 * @(#)MailServiceException.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.mail;

/**
 * Class MailServiceException (MailServiceException.java)
 * RuntimeException thrown when properties are not valid
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 12 Sep 2018
 */
public class MailServiceException extends RuntimeException
{
    /**
     * @param message error message
     */
    public MailServiceException(String message)
    {
        super(message);
    }

    /**
     * @param message error message
     * @param cause error reason
     */
    public MailServiceException(String message, Throwable cause)
    {
        super(message, cause);
    }
}
