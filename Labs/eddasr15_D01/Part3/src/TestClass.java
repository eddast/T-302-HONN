/*
 * @(#)TestClass.java 1.0 22 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

/**
 * Application TestClass (TestClass.java)
 * Tests classes functionality of Employee.java og Manager.java
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 22 Aug 2018
 * Licensed under the Open Software License version 3.0
 * (http://opensource.org/licenses/OSL-3.0)
 */
public class TestClass
{
    public static void main(String[] args)
    {
        // Prints one employee and one manager
        Employee e0 = new Employee("Dilbert");
        Employee e1 = new Manager("Pointy Haired", "Boss");
        System.out.println("e0: " + e0);
        System.out.println("e1: " + e1);

        // Prints out list of employees
        Employee elist[] = new Employee[2];
        elist[0] = e0;
        elist[1] = e1;
        for (int i = 0; i < elist.length; i++)
        {
            System.out.println(i + ":" + elist[i]);
        }

        // print out job title for managers only in employee list
        for (int i = 0; i < 2; i++)
        {
            System.out.println(elist[i].getName());
            if (elist[i] instanceof Manager)
            {
                Manager m = (Manager) elist[i];
                System.out.println("\t" + m.getTitle());
            }
        }



    }
}
