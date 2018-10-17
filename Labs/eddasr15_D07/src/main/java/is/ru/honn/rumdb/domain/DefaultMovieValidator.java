/*
 * @(#)DefaultMovieValidator.java 1.0 10 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.rumdb.domain;

/**
 * Class DefaultMovieValidator (DefaultMovieValidator.java)
 * Validates a movie object by ensuring title and link have values
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 10 Oct 2018
 */
public class DefaultMovieValidator implements Validator
{
    /**
     * Movie to validate
     */
    private Movie movie;

    /**
     * Constructs validator for a movie object
     *
     * @param movie movie associated with validator
     */
    public DefaultMovieValidator(Movie movie)
    {
        this.movie = movie;

    }
    /**
     * Provides validation for Movie
     *
     * @return true if movie is valid e.g. has title and link, false otherwise
     */
    public boolean validate()
    {
        return
        (
            movie.getTitle() != null && movie.getTitle().length() != 0 &&
            movie.getLink() != null && movie.getLink().length() != 0
        );
    }
}
