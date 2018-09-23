/*
 * @(#)Person.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn;

/**
 * Application Person (Person.java)
 * Implements a data structure of person
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 12 Sep 2018
 */
public class Person
{
    /**
     * person's name
     */
    private String name;

    /**
     * person's email address
     */
    private String email;

    /**
     *
     */
    public Person ()
    {

    }

    /**
     * @param name person's name
     * @param email person's email
     */
    public Person (String name, String email)
    {
        this.name = name;
        this.email = email;
    }

    /**
     * @param name new name for person
     */
    public void setName(String name)
    {
        this.name = name;
    }

    /**
     * @return name of person
     */
    public String getName()
    {
        return name;
    }

    /**
     * @param email new email for person
     */
    public void setEmail(String email)
    {
        this.email = email;
    }

    /**
     * @return person's email
     */
    public String getEmail()
    {
        return email;
    }

    @Override
    public String toString() {
        return this.name + ", " + this.email;
    }
}
