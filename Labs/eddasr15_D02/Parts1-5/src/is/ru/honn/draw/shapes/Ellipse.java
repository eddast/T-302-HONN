/*
 * @(#)Ellipse.java 1.0 29 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

package is.ru.honn.draw.shapes;

import java.awt.*;

/**
 * Class Ellipse (Ellipse.java)
 * Inherits shape and represents an oval (ellipse) shape
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 29 Aug 2018
 */
public class Ellipse extends Shape
{
    private int width, height;

    /**
     * Constructs an ellipse
     *
     * @param x x coordinate
     * @param y y coordinate
     * @param width width or major radius
     * @param height height or minor radius
     * @param color ellipse color
     */
    public Ellipse(int x, int y, int width, int height, Color color)
    {
        super.x = x;
        super.y = y;
        this.height = height;
        this.width = width;
        super.color = color;
    }

    @Override
    public void draw(Graphics g)
    {
        g.setColor(color);
        g.drawOval(x, y, width, height);
    }
}