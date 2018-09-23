/*
 * @(#)Employee.java 1.0 29 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */


package is.ru.honn;

import java.util.Date;

import java.text.SimpleDateFormat;

/**
 * Class Employee (Employee.java)
 * Constructs Employee as extension of person with added hire date
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 29 Aug 2018
 */
public class Employee extends Person implements Worktime
{
    protected Date dateOfHire;

    protected double salary;


    /**
     * constructs employee no information
     */
    public Employee()
    {

    }

    /**
     * constructs employee with name and date of birth and date of hire
     *
     * @param name
     * @param dateOfBirth
     * @param dateOfHire
     * @param salary
     */
    public Employee(String name, Date dateOfBirth, Date dateOfHire, double salary)
    {
        this.name = name;
        this.dateOfBirth = dateOfBirth;
        this.dateOfHire = dateOfHire;
        this.salary = salary;
    }

    /**
     * @param dateOfHire new date of hire to assign to employee
     */
    public void setDateOfHire(Date dateOfHire)
    {
        this.dateOfHire = dateOfHire;
    }

    /**
     * @return employee's date of hire
     */
    public Date getDateOfHire()
    {
        return this.dateOfHire;
    }

    @Override
    public double getSalary()
    {
        return salary;
    }

    /**
     * @param salary new salary to assign to employee
     */
    public void setSalary(int salary)
    {
        this.salary = salary;
    }

    /**
     * @return employee stringified (with name and hire date)
     */
    public String toString()
    {
        // Add date format (as per part 8)
        SimpleDateFormat sdf = new SimpleDateFormat("dd.mm.yyyy");
        return "Employee: " + getName() + ", hired " + sdf.format(getDateOfHire());
    }


    @Override
    public int getWorkDays(Date now)
    {
        return (int)((now.getTime() - dateOfHire.getTime()) / 86400000);
    }

}
