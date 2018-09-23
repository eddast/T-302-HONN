/*
 * @(#)FactoryException.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.factory;

/**
 * Class FactoryException (FactoryException.java)
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 12 Sep 2018
 */
public class FactoryException extends Exception
{
    /**
     * Initiates a new FactoryException with message
     * @param message the message to set the exception with
     */
    public FactoryException(String message)
    {
        super(message);
    }

    /**
     * Initiates a new FactoryException with message and a cause
     * @param message the message to set the exception with
     * @param cause the cause of the exception
     */
    public FactoryException(String message, Throwable cause)
    {
        super(message, cause);
    }
}
