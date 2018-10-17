/*
 * @(#)TestSimple.java 1.0 10 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.rumdb.test;

import is.ru.honn.rumdb.data.MovieDataGatewayStub;
import is.ru.honn.rumdb.domain.Movie;
import is.ru.honn.rumdb.mail.MailServerStub;
import is.ru.honn.rumdb.service.MovieService;
import is.ru.honn.rumdb.service.ServiceException;

import javax.xml.ws.Service;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.List;

/**
 * Application TestSimple (TestSimple.java)
 * Tests the Movie class and Movie Service class
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 10 Oct 2018
 */
class TestSimple
{

    public static void main(String[] args)
    {
        new TestSimple();
    }

    /**
     * Tests the Movie class by incrementing views and adding ratings
     */
    public TestSimple()
    {
        /* Test single movie POJO */
        System.out.println("TESTING A SINGLE MOVIE CLASS");
        Movie movie = new Movie(1, "Skyfall",
                "http:// http://www.skyfall-movie.com/site/ ",
                "Bond's loyalty to M is tested as her past comes back to haunt her.",
                new GregorianCalendar(2012, Calendar.OCTOBER, 26).getTime(), "Sam Mendes");
        movie.view();
        movie.rate(8);
        System.out.println(movie);

        /* Test movie service class */
        /* Also test movie service mail gateway, e.g. that it sends mail when movie is added */
        System.out.println("TESTING MOVIE SERVICE CLASS WITH MAIL GATEWAY");
        MovieService movieService = new MovieService();
        movieService.setMovieDataGateway(new MovieDataGatewayStub());
        movieService.setMailGateway(new MailServerStub());
        try
        {
            movieService.addMovie(new Movie(1, "Movie 1", "http1", "", new Date(), ""));
            movieService.addMovie(new Movie(1, "Movie 2", "http2", "", new Date(), ""));
            movieService.addMovie(new Movie(1, "Movie 3", "http3", "", new Date(), ""));
            movieService.addMovie(new Movie(1, "Movie 4", "http4", "", new Date(), ""));
            movieService.addMovie(new Movie(1, null, "http1", "", new Date(), ""));
        }
        catch (ServiceException sex) // you dirty dog!
        {
            System.out.println(sex.getMessage());
        }
        List<Movie> list = movieService.getMovies();
        for (Movie m: list) System.out.println(m);
        System.out.println("===========================\n\n");

    }
}
