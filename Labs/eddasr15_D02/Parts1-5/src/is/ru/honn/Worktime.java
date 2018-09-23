/*
 * @(#)Manager.java 1.0 29 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

package is.ru.honn;

import java.util.Date;

/**
 * Interface Worktime (Worktime.java)
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 29 Aug 2018
 */
public interface Worktime
{
    /**
     * @param now today's date
     * @return total days of work from now date from hire date
     */
    public int getWorkDays(Date now);
}
