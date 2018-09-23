/*
 * @(#)Teiknir.java 1.0 29 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.draw;

import is.ru.honn.draw.shapes.Rectangle;
import is.ru.honn.draw.shapes.Ellipse;
import is.ru.honn.draw.shapes.Triangle;

import javax.swing.*;
import java.awt.*;

/**
 * Application Teiknir (Teiknir.java)
 * Tests drawables and canvas (page)
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 29 Aug 2018
 */
class Teiknir extends JPanel
{
    private Page page = new Page();

    /**
     * Adds shapes to canvas (page)
     */
    public Teiknir ()
    {
        page.add(new Rectangle(100, 100, 200, 150, Color.blue));
        page.add(new Rectangle(50, 50, 80, 44, Color.green));
        page.add(new Ellipse(150, 150, 100, 20, Color.pink));
        page.add(new Ellipse(250, 30, 200, 200, Color.yellow));
        page.add(new Triangle(350, 300, 100, Color.red));
        page.add(new Triangle(30, 40, 20, Color.black));
    }

    public static void main (String[] argv)
    {
        JFrame f = new JFrame ();

        f.setTitle("Teiknir");
        f.setResizable(true);
        f.setSize(500, 500);

        Teiknir panel = new Teiknir ();
        f.getContentPane().add(panel);
        f.setVisible(true);
    }

    @Override
    public void paint(Graphics g)
    {
        super.paint(g);
        this.setBackground(Color.white);
        page.paint(g);
    }
}
