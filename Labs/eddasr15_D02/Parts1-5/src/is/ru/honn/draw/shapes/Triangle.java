/*
 * @(#)Triangle.java 1.0 29 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

package is.ru.honn.draw.shapes;

import java.awt.*;

/**
 * Class Triangle (Triangle.java)
 * Inherits shape and represents a triangle shape
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 29 Aug 2018
 */
public class Triangle extends Shape
{
    private int size;

    /**
     * Constructs a equal triangle from center point
     *
     * @param x x coordinate center to triangle
     * @param y y coordinate center to triangle
     * @param size length (in coordinates) from center to corners
     */
    public Triangle(int x, int y, int size, Color color)
    {
        super.x = x;
        super.y = y;
        this.size = size;
        this.color = color;
    }

    @Override
    public void draw(Graphics g)
    {
        g.setColor(color);

        // construct points based on size
        int x[]={this.x,        this.x+size,    this.x-size};
        int y[]={this.y-size,   this.y+size,    this.y+size};

        g.drawPolygon(x,y,3);
    }
}
