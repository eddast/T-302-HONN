/*
 * @(#)Employee.java 1.0 22 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

/**
 * Class Employee (Employee.java)
 * Abstract structure representing employee with a name
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 22 Aug 2018
 * Licensed under the Open Software License version 3.0
 * (http://opensource.org/licenses/OSL-3.0)
 */
public class Employee
{
    /**
     * Employee name
     */
    private String name;

    /**
     * constructs new employee without a name
     */
    public Employee()
    {

    }

    /**
     * constructs new employee with a name
     *
     * @param name name of employee
     *
     */
    public Employee(String name)
    {
        this.name = name;
    }

    /**
     * @return employee name
     */
    public String getName()
    {
        return name;
    }

    /**
     * toString override
     *
     * @return string equivalent to define how Employee variables are printed
     */
    public String toString()
    {
        return "Employee: " + getName();
    }
}
