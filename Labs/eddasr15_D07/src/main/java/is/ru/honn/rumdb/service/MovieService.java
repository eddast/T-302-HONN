/*
 * @(#)MovieService.java 1.0 10 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.rumdb.service;

import is.ru.honn.rumdb.data.MovieDataGateway;
import is.ru.honn.rumdb.domain.Movie;

import java.util.List;

/**
 * Class MovieService (MovieService.java)
 * Operational Script class - layer for calling functions in data gateway to manipulate data source
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 10 Oct 2018
 */
public class MovieService extends ApplicationService
{
    /**
     * Gateway used to manipulate movie data from data source
     */
    private MovieDataGateway movieDataGateway;

    /**
     * Adds new movie to data source
     *
     * @param movie movie to add
     */
    public void addMovie(Movie movie) throws ServiceException
    {
        movie.initialize();
        if(!movie.validate()) throw new ServiceException("Movie could not be added - Movie was not valid");
        movieDataGateway.addMovie(movie);
        this.getMailGateway().sendMessage("sender", "reciever", "new movie added", "new movie " + movie.getTitle() + " was added");
    }
    /**
     * @return all movies from data source
     */
    public List<Movie> getMovies()
    {
        return movieDataGateway.getMovies();
    }
    /**
     * @param movieDataGateway which data source gateway to use
     */
    public void setMovieDataGateway(MovieDataGateway movieDataGateway)
    {
        this.movieDataGateway = movieDataGateway;
    }
}
