/*
 * @(#)Reikna.java 1.0 22 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

/**
 * Application Reikna (Reikna.java)
 * Tests Point class by initializing Point objects and printing them
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 22 Aug 2018
 * Licensed under the Open Software License version 3.0
 * (http://opensource.org/licenses/OSL-3.0)
 */
public class Reikna
{
    public static void main (String[] args)
    {
        // initializes and prints string from single point object
        /*
        Point p = new Point(4.0, 3.0);
        String hnit = p.skrifa();
        System.out.println(hnit);
        */

        // initialize 100 points in point array
        int size = 100;
        Point[] kurfa = new Point[size];
        for(int i = 0; i < size; i++)
        {
            // x coordinates range from {sin(0.01), sin(0.02) ... sin(1)}
            // y coordinates range from {cos(0.01), cos(0.02) ... cos(1)}
            double px = Math.sin((float)(i+1)/size);
            double py = Math.cos((float)(i+1)/size);
            Point p = new Point(px,py);
            kurfa[i] = p;
        }
        for(int i = 0; i < kurfa.length; i++)
        {
            System.out.println(kurfa[i].skrifa());
        }
    }

}
