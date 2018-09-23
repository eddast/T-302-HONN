/*
 * @(#)Test1.java 1.0 29 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Date;

// imports for creating "mock"/random data
import java.util.GregorianCalendar;
import java.math.BigDecimal;
import java.math.RoundingMode;

/**
 * Application Test1 (Test1.java)
 * Tests functionality of Person, Employee and Manager classes
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 29 Aug 2018
 */
public class Test1
{
    public static void main (String[] args)
    {
        ArrayList<Person> staff = new ArrayList<Person>();

        staff.add(new Employee( "Danny", getRandomBirthDate(), getRandomHireOrTitleDate(), getRandomSalary()));
        staff.add(new Employee( "Grace", getRandomBirthDate(), getRandomHireOrTitleDate(), getRandomSalary()));
        staff.add(new Employee( "Fred", getRandomBirthDate(), getRandomHireOrTitleDate(), getRandomSalary()));
        staff.add(new Employee( "Henri", getRandomBirthDate(), getRandomHireOrTitleDate(), getRandomSalary()));
        staff.add(new Employee( "Erika", getRandomBirthDate(), getRandomHireOrTitleDate(), getRandomSalary()));
        staff.add(new Manager( "Ida", "The Boss", getRandomBirthDate(), getRandomHireOrTitleDate(), getRandomSalary()));

        // PERSON IS ABSTRACT AND CANNOT BE INSTANTIATED
        // staff.add(new Person( "Leslie", getRandomBirthDate()));

        // Using Collections to sort ArrayList before printing it
        Collections.sort(staff);

        // Print employee list as a person type
        for (Object aStaff : staff)
        {
            Person p = (Person) aStaff;

            // System.out.println(p);

            // System.out.println(p.getName() + ": "+ p.getSalary());

            Worktime w = (Worktime)aStaff;
            System.out.println(p.getName() + ": " + w.getWorkDays(new Date()));

        }
    }

    /**
     * @return random salary between 1000.0-10000.0$ (with at most 2 decimal points)
     */
    private static double getRandomSalary()
    {
        return BigDecimal.valueOf(1000 + Math.random() * (10000 - 1000)).setScale(2, RoundingMode.HALF_UP).doubleValue();
    }

    /**
     * @return random birth date, defined as some date between the years of 1945-1995
     */
    private static Date getRandomBirthDate()
    {
        return getRandomDate(1945, 1995);
    }

    /**
     * @return random hire or title date, defined as some date between the years of 1995-2018
     */
    private static Date getRandomHireOrTitleDate()
    {
        return getRandomDate(1995, 2018);
    }

    /**
     * @param yearRangeLower the lower bound of years in range
     * @param yearRangeUpper the upper bound of years in range
     *
     * @return random date in the specified year range
     */
    private static Date getRandomDate(int yearRangeLower, int yearRangeUpper)
    {
        GregorianCalendar gc = new GregorianCalendar();

        int year = yearRangeLower + (int)Math.round(Math.random() * (yearRangeUpper - yearRangeLower));
        int dayOfYear = 1 + (int)Math.round(Math.random() * (gc.getActualMaximum(gc.DAY_OF_YEAR) - 1));

        gc.set(gc.YEAR, year);
        gc.set(gc.DAY_OF_YEAR, dayOfYear);

        return gc.getTime();
    }
}

