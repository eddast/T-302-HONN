/*
 * @(#)Page.java 1.0 29 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

package is.ru.honn.draw;

import is.ru.honn.draw.shapes.Shape;

import java.awt.Graphics;
import java.util.ArrayList;

/**
 * Class Page (Page.java)
 * Sets up canvas for drawables
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 29 Aug 2018
 */
public class Page
{
    ArrayList<Shape> shapes = new ArrayList<Shape>();

    /**
     * Adds shape to canvas (page) list
     *
     * @param s shape to be added to canvas (page)
     */
    public void add(Shape s)
    {
        shapes.add(s);
    }

    /**
     * Paints (draws) on canvas (page)
     *
     * @param g graphic (drawable) to be painted on canvas
     */
    public void paint(Graphics g)
    {
        for(Drawable d: shapes)
        {
            d.draw(g);
        }
    }
}
