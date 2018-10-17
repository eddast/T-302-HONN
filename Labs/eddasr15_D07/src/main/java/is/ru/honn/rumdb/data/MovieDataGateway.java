/*
 * @(#)MovieDataGateway.java 1.0 10 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.rumdb.data;

import is.ru.honn.rumdb.domain.Movie;

import java.util.List;

/**
 * Interface MovieDataGateway (MovieDataGateway.java)
 * Defines actions a gateway to fetch movies should have
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 10 Oct 2018
 */
public interface MovieDataGateway
{
    /**
     * @param movie movie to add to data source
     */
    public void addMovie(Movie movie);

    /**
     * @return a list of all movies from data source
     */
    public List<Movie> getMovies();
}

