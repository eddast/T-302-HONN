/*
 * @(#)Validator.java 1.0 10 Oct 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

package is.ru.honn.rumdb.domain;

/**
 * Interface Validator (Validator.java)
 * Validates a data object
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 10 Oct 2018
 */
public interface Validator
{
    /**
     * Provides validation for object
     *
     * @return true if object is valid, false otherwise
     */
    public boolean validate();
}
