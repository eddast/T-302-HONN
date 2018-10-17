/*
 * @(#)Movie.java 1.0 10 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
package is.ru.honn.rumdb.domain;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

/**
 * Class Movie (Movie.java)
 * Domain model representing a movie
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 10 Oct 2018
 */
public class Movie implements Comparable<Movie>
{
    /**
     * Movie id
     */
    private int id;
    /**
     * Movie title
     */
    private String title;
    /**
     * URL link to movie description and info
     */
    private String link;
    /**
     * Movie description
     */
    private String description;
    /**
     * Movie release date
     */
    private Date release;
    /**
     * Movie director
     */
    private String director;
    /**
     * Movie rating
     */
    private Rating rating = new Rating();
    /**
     * Validators for validating the movie
     */
    private List<Validator> validators = new ArrayList<Validator>();

    /**
     * Constructs empty movie object
     */
    public Movie()
    {

    }
    /**
     * Constructs a movie object with values
     *
     * @param id
     * @param title
     * @param link
     * @param description
     * @param release
     * @param director
     */
    public Movie(int id, String title, String link, String description, Date release, String director)
    {
        this.id = id;
        this.title = title;
        this.link = link;
        this.description = description;
        this.release = release;
        this.director = director;
    }
    /**
     * Initializes ratings for movie
     */
    public void initialize()
    {
        this.rating.reset();
        clearValidators();
        addValidator(new DefaultMovieValidator(this));
    }
    /**
     * Increments view for a movie by one
     */
    public void view()
    {
        this.rating.incrementView();
    }
    /**
     * Rates a given movie
     *
     * @param rate the int value of rating
     */
    public void rate (int rate)
    {
        this.rating.incrementRating(rate);
    }
    /**
     * @return average rate for a movie (e.g. total rating/number of rates)
     */
    public double getAverageRate()
    {
        return this.rating.getAverageRating();
    }
    /**
     * Compares this object with the specified object for order.
     * Order set by Movie's id
     *
     * @return 1, 0 or -1 indicating whether current movie is larger, smaller or equal to compared movie
     * @throws NullPointerException if the specified object is null
     * @throws ClassCastException   if type errors occur
     */
    public int compareTo(Movie o)
    {
        if(this.id > o.id) return 1;
        if(o.id < this.id) return -1;
        return 0;
    }
    /**
     * @return true if movie is valid according to all embedded validators
     */
    public boolean validate()
    {
        for (Validator v: validators) if(!v.validate()) return false;
        return true;
    }
    /**
     * clears all associated validators for a movie
     */
    public void clearValidators()
    {
        this.validators = new ArrayList<Validator>();
    }
    /**
     * @param validator validator to add to list of validators that validate movie
     */
    public void addValidator(Validator validator)
    {
        this.validators.add(validator);
    }
    /**
     * @return string equivalent of movie
     */
    public String toString()
    {
        String movie = "\n----------------MOVIE---------------------\n";
        movie += "Title: " + this.title;
        movie += "\nID: " + this.id;
        movie += "\nLink: " + this.link;
        movie += "\nDescription: " + this.description;
        movie += "\nRelease date: " + this.release;
        movie += "\nDirected by: " + this.director;
        movie += "\n" + this.rating;
        movie += "\n------------------------------------------\n";

        return movie;
    }
    /**
     * @return id of the movie
     */
    public int getId()
    {
        return id;
    }
    /**
     * @return title of the movie
     */
    public String getTitle() {
        return title;
    }
    /**
     * @param title new title to set for movie
     */
    public void setTitle(String title) {
        this.title = title;
    }
    /**
     * @return URL to link for movie
     */
    public String getLink()
    {
        return link;
    }
    /**
     * @param link new URL link to set for movie
     */
    public void setLink(String link)
    {
        this.link = link;
    }
    /**
     * @return description of the movie
     */
    public String getDescription()
    {
        return description;
    }
    public void setDescription(String description)
    {
        this.description = description;
    }
    /**
     * @return release date of the movie
     */
    public Date getRelease()
    {
        return release;
    }
    /**
     * @param release new release date to set for movie
     */
    public void setRelease(Date release)
    {
        this.release = release;
    }
    /**
     * @return director of the movie
     */
    public String getDirector()
    {
        return director;
    }
    /**
     * @param director new director to set for movie
     */
    public void setDirector(String director)
    {
        this.director = director;
    }
    /**
     * @return movie's rating
     */
    public Rating getRating()
    {
        return rating;
    }
    /**
     * @param rating new rating to set for movie
     */
    public void setRating(Rating rating)
    {
        this.rating = rating;
    }
}
