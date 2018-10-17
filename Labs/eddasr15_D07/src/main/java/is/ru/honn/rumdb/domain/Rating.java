/*
 * @(#)Rating.java 1.0 10 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

package is.ru.honn.rumdb.domain;

/**
 * Class Rating (Rating.java)
 * Domain model representing rating for some movie
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 10 Oct 2018
 */
public class Rating {
    /**
     * Number of requests
     */
    private int views;
    /**
     * Number of rates
     */
    private int rates;
    /**
     * Combined value of ratings
     */
    private int ratings;

    /**
     * Constructs empty rating
     */
    public Rating()
    {

    }
    /**
     * Constructs a rating
     *
     * @param views number of requests for movie
     * @param rates number of rates for movie
     * @param ratings combined rates for movie
     */
    public Rating(int views, int rates, int ratings)
    {
        this.views = views;
        this.rates = rates;
        this.ratings = ratings;
    }
    /**
     * Increments number of requests for movie by one
     */
    public void incrementView()
    {
        this.views += 1;
    }
    /**
     * Increments number of rating by n for movie
     */
    public void incrementRating(int n)
    {
        this.ratings += n;
    }
    /**
     * Gets the average rating for a movie
     */
    public double getAverageRating()
    {
        return (double)this.ratings/(double)this.rates;
    }
    /**
     * Resets rating's values
     */
    public void reset()
    {
        this.views = 0;
        this.rates = 0;
        this.ratings = 0;
    }
    /**
     * @return string equivalent of rating
     */
    public String toString()
    {
        return "Views: " + this.views + ", Rates: " + this.rates + " Combined Rates: " + this.ratings;
    }
    /**
     * @return number of requests for movie
     */
    public int getViews()
    {
        return views;
    }
    /**
     * @param views new number of requests to set for movie
     */
    public void setViews(int views)
    {
        this.views = views;
    }
    /**
     * @return number of rates for movie
     */
    public int getRates()
    {
        return rates;
    }
    /**
     * @param rates new number of rates to set for movie
     */
    public void setRates(int rates)
    {
        this.rates = rates;
    }
    /**
     * @return value of combined rates for movie
     */
    public int getRatings()
    {
        return ratings;
    }
    /**
     * @param ratings new ratings to set for movie
     */
    public void setRatings(int ratings)
    {
        this.ratings = ratings;
    }
}
