/*
 * @(#)Rectangle.java 1.0 29 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

package is.ru.honn.draw.shapes;

import java.awt.*;

/**
 * Class Rectangle (Rectangle.java)
 * Inherits shape and represents a rectangle shape
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 29 Aug 2018
 */
public class Rectangle extends Shape
{
    private int width, height;

    /**
     * Constructs a rectangle
     *
     * @param x x coordinate
     * @param y y coordinate
     * @param width rect width
     * @param height rect height
     * @param color rect color
     */
    public Rectangle(int x, int y, int width, int height, Color color)
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
        g.drawRect(x, y, width, height);
    }
}
