/*
 * @(#)MailFactory.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.mail;

import is.ru.honn.factory.AbstractFactory;
import is.ru.honn.factory.FactoryException;

/**
 * Class MailFactory (MailFactory.java)
 * Gets mail service object from properties
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 12 Sep 2018
 */
public class MailFactory extends AbstractFactory
{
    /**
     * @return mail service object from properties if they're able to load
     */
    public MailService getMailService()
    {
        MailService service;
        try
        {
            loadProperties("mail.properties");
            service = (MailService) loadClass(
                    getProperties().getProperty("mail.service.class"));
            service.setMailServer(getProperties().getProperty("mail.server"));
        }
        catch (FactoryException fex)
        {
            throw new MailServiceException("Unable to load class", fex);
        }
        return service;
    }
}
