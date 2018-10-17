/*
 * @(#)ServiceException.java 1.0 10 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.rumdb.service;

/**
 * Class ServiceException (ServiceException.java)
 * Exception thrown when service encounters an error it cannot retrieve from
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 10 Oct 2018
 */
public class ServiceException extends Exception
{
    public ServiceException()
    {
        super();
    }

    public ServiceException(String s)
    {
        super(s);
    }

    public ServiceException(String s, Throwable throwable)
    {
        super(s, throwable);
    }
}
