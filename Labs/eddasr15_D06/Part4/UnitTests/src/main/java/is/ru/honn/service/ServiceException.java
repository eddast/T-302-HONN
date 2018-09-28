/*
 * @(#)ServiceException.java 1.0 4 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.service;

/**
 * Class ServiceException (ServiceException.java)
 * Custom exception thrown when a system service cannot function correctly
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 4 Oct 2018
 */
public class ServiceException extends Exception
{
    public ServiceException(String message)
    {
        super(message);
    }
    public ServiceException(String message, Throwable cause)
    {
        super(message, cause);
    }
}
