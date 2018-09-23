/*
 * @(#)Manager.java 1.0 22 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

/**
 * Class Manager (Manager.java)
 * inherits from Employee.java and has additional attribute job title
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 22 Aug 2018
 * Licensed under the Open Software License version 3.0
 * (http://opensource.org/licenses/OSL-3.0)
 */
public class Manager extends Employee
{
    /**
     * Job title
     */
    private String title;

    /**
     * Constructs new Employee as Manager and adds a job title
     *
     * @param name manager name
     * @param title manager job title
     *
     */
    public Manager(String name, String title)
    {
        super(name);
        this.title = title;
    }

    /**
     * @return job title
     */
    public String getTitle()
    {
        return title;
    }

    /**
     * toString override defines how Manager variables are printed
     *
     * @return string equivalent to define how Employee variables are printed
     */
    public String toString()
    {
        return "Manager: " + getName() + ", " + getTitle();
    }
}
