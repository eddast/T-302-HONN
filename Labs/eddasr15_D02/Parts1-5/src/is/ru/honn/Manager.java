/*
 * @(#)Manager.java 1.0 29 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */


package is.ru.honn;

import java.util.Date;

/**
 * Class Manager (Manager.java)
 * Constructs Manager as extension of employee with added title
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 29 Aug 2018
 */
public class Manager extends Employee
{
    protected String title;

    /**
     * constructs manager no information
     */
    public Manager()
    {

    }

    /**
     * constructs manager with name and date of birth and date of hire
     *
     * @param name
     * @param title
     * @param dateOfBirth
     * @param dateOfHire
     */
    public Manager(String name, String title, Date dateOfBirth, Date dateOfHire, double salary)
    {
        this.name = name;
        this.title = title;
        this.dateOfBirth = dateOfBirth;
        this.dateOfHire = dateOfHire;
        this.salary = salary;
    }


    /**
     * @param title new title to assign to manager
     */
    public void setTitle(String title)
    {
        this.title = title;
    }

    /**
     * @return manager's title
     */
    public String getTitle()
    {
        return this.title;
    }

    /**
     * @return manager stringified (with name and title)
     */
    public String toString()
    {
        return "Manager: " + getName() + ", " + getTitle();
    }

}
