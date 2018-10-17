/*
 * @(#)MovieDataGatewayStub.java 1.0 10 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.rumdb.data;

import is.ru.honn.rumdb.domain.Movie;

import java.util.ArrayList;
import java.util.List;

/**
 * Class MovieDataGatewayStub (MovieDataGatewayStub.java)
 * A mock class, acts as an in-memory movie server
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 10 Oct 2018
 */
public class MovieDataGatewayStub implements MovieDataGateway {

    /**
     * Movies currently in stub
     */
    List<Movie> movies;

    /**
     * Initializes empty list of movies
     */
    public MovieDataGatewayStub()
    {
        this.movies = new ArrayList<Movie>();
    }
    /**
     * @param movie movie to add to data source
     */
    public void addMovie(Movie movie)
    {
        this.movies.add(movie);
    }
    /**
     * @return a list of all movies from data source
     */
    public List<Movie> getMovies()
    {
        return this.movies;
    }
}
