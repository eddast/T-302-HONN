/*
 * @(#)ApplicationService.java 1.0 10 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.rumdb.service;

import is.ru.honn.rumdb.mail.MailGateway;

/**
 * Class ApplicationService (ApplicationService.java)
 * Sends a mail via mail gateway
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 10 Oct 2018
 */
public class ApplicationService
{
    /**
     * Mail gateway used by service
     */
    private MailGateway mailGateway;

    /**
     * @return the mail gateway to use
     */
    public MailGateway getMailGateway()
    {
        return mailGateway;
    }
    /**
     * @param mailGateway which mail gateway to use
     */
    public void setMailGateway(MailGateway mailGateway)
    {
        this.mailGateway = mailGateway;
    }
}
