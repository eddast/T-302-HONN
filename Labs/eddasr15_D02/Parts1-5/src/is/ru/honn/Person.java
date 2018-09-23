/*
 * @(#)Person.java 1.0 29 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

package is.ru.honn;

import java.util.Date;

/**
 * Class Person (Person.java)
 * Constructs person with name and birth date
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 29 Aug 2018
 */
abstract public class Person implements Comparable
{
    protected String name;

    protected Date dateOfBirth;

    /**
     * constructs person without name and birth date
     */
    public Person()
    {

    }

    /**
     * constructs person with name and date of birth
     *
     * @param name person's name
     * @param dateOfBirth person's date of birth
     */
    public Person(String name, Date dateOfBirth)
    {
        this.name = name;
        this.dateOfBirth = dateOfBirth;
    }

    /**
     * @param name new name to assign to person
     */
    public void setName(String name)
    {
        this.name = name;
    }

    /**
     * @return person's name
     */
    public String getName()
    {
        return this.name;
    }

    /**
     * @param dateOfBirth new birth date to assign to person
     */
    public void setDateOfBirth(Date dateOfBirth)
    {
        this.dateOfBirth = dateOfBirth;
    }

    /**
     * @return person's date of birth
     */
    public Date getDateOfBirth()
    {
        return this.dateOfBirth;
    }

    /**
     * @return person stringified (with name)
     */
    public String toString ()
    {
        return "Person: " + getName();
    }

    /**
     * @param o person (including subclasses) to compare to
     * @return comparison value based on person names
     */
    public int compareTo(Object o) {
        Person p = (Person)o;
        return this.name.compareTo(p.getName());
    }

    /**
     * @return person's salary
     */
    public abstract double getSalary();
}
