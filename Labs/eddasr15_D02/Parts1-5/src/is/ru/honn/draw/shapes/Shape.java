/*
 * @(#)Shape.java 1.0 29 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

package is.ru.honn.draw.shapes;

import is.ru.honn.draw.Drawable;

import java.awt.*;

/**
 * Abstract class Shape (Shape.java)
 * Holds x and y coordinates and color of shapes i.e. subclasses
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 29 Aug 2018
 */
abstract public class Shape implements Drawable
{
    protected int x, y;

    protected Color color;

    public abstract void draw(Graphics g);
}