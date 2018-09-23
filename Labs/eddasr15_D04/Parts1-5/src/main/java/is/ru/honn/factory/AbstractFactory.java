/*
 * @(#)AbstractFactory.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.factory;

import java.io.FileInputStream;

import java.io.IOException;
import java.io.File;
import java.io.FileNotFoundException;
import java.util.Properties;
import java.util.logging.Logger;

/**
 * Class AbstractFactory (AbstractFactory.java)
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 12 Sep 2018
 */
public abstract class AbstractFactory
{
    /**
     * logs error message for class
     */
    private Logger logger =
            Logger.getLogger(AbstractFactory.class.getName());

    /**
     * The properties class fetches
     */
    private Properties properties = new Properties();

    /**
     * Loads file from properties from properties file
     * @param filename name of file
     * @return the properties
     * @throws FactoryException if file reading fails
     */
    protected Properties loadProperties(String filename)
            throws FactoryException
    {

        Properties props = new Properties();
        try
        {
            properties.load(new FileInputStream(new File(filename)));
        }
        catch (FileNotFoundException fnfex)
        {
            String msg = "Factory: File '" + filename + "' not found.";
            logger.severe(msg);
            throw new FactoryException(msg, fnfex);
        }
        catch (IOException ioex)
        {
            String msg = "Factory: Unable to read file '" + filename + "'.";
            logger.severe(msg);
            throw new FactoryException(msg, ioex);
        }
        return properties;
    }

    /**
     * @return properties of current instance
     */
    public Properties getProperties()
    {
        return properties;
    }


    /**
     * @param name name of class to instansiate
     * @return new instance of class from property file
     * @throws FactoryException if class could not be instansiated
     */
    protected Object loadClass(String name) throws FactoryException
    {
        Class instanceClass;
        try
        {
            instanceClass = Class.forName(name);
            return instanceClass.newInstance();
        }
        catch (ClassNotFoundException cnfex)
        {
            String msg = "Factory: Class '" + name + "' not found.";
            logger.severe(msg);
            throw new FactoryException(msg, cnfex);
        }
        catch (InstantiationException iex)
        {
            String msg = "Factory: Instancing class '" + name + "' failed.";
            logger.severe(msg);
            throw new FactoryException(msg, iex);
        }
        catch (IllegalAccessException iaex)
        {
            String msg = "Factory: Illegal access instanciating class '" +
                    name + "'.";
            logger.severe(msg);
            throw new FactoryException(msg, iaex);
        }
    }
}
