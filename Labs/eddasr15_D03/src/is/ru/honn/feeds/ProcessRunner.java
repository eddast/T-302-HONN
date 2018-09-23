/*
 * @(#)ProcessRunner.java 1.0 5 Sep 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.feeds;

/**
 * Application ProcessRunner (ProcessRunner.java)
 * Runs a new ReaderProcess that processes some feed from an URL
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 5 Sep 2018
 */
public class ProcessRunner
{
    public static void main(String[] args)
    {
        ReaderProcess readerProcess = new ReaderProcess();
        readerProcess.read();
    }
}