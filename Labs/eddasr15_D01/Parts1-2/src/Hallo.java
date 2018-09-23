/*
 * @(#)Hallo.java 1.0 22 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

import java.lang.Object;

/**
 * Application Hallo (Hallo.java)
 * Writes out good advice and command line arguments provided
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 22 Aug 2018
 * Licensed under the Open Software License version 3.0
 * (http://opensource.org/licenses/OSL-3.0)
 */
public class Hallo extends Object
{
    public static void main (String[] args)
    {
        String heilraedi = "Gott er þeim sem glatt hafa sinni,\n" +
                "guð sé með oss öllum hér inni.\n";

        // prints good advice
        System.out.println (heilraedi);

        // prints command line arguments
        for (int i=0; i < args.length; i++)
        {
            System.out.println (args[i]);
        }
    }
}
