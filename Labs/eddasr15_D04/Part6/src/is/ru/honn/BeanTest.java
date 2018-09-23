/*
 * @(#)BeanTest.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn;

import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

/**
 * Application BeanTest (BeanTest.java)
 * Tests getBeans() for initiating person instance from spring config file
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 12 Sep 2018
 */
public class BeanTest
{
    public static void main(String[] args)
    {
        ApplicationContext context= new
                ClassPathXmlApplicationContext("/spring-config.xml");
        Person person = (Person) context.getBean("person");
        System.out.println(person);

    }
}
