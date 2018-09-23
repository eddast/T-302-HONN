/*
 * @(#)Point.java 1.0 22 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

/**
 * Class Point (Point.java)
 * Abstract structure representing a point in 2D space with x and y coordinates
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 22 Aug 2018
 * Licensed under the Open Software License version 3.0
 * (http://opensource.org/licenses/OSL-3.0)
 */
public class Point
{
    /**
     * x coordinate of point
     */
    private double dx;

    /**
     * y coordinate of point
     */
    private double dy;

    /**
     * constructs point with x and y coordinates
     *
     * @param dx x-coordinate
     * @param dy y-coordinate
     */
    public Point(double dx, double dy)
    {
        this.dx = dx;
        this.dy = dy;
    }

    /**
     * @return the x-coordinate
     */
    public double getX()
    {
        return dx;
    }

    /**
     * @return the y-coordinate
     */
    public double getY()
    {
        return dy;
    }

    /**
     * @return x and y coordinates of point in form of string
     */
    public String skrifa()
    {
        return "Hnit (" + dx + ", " + dy +")";
    }
}