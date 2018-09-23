/*
 * @(#)JsonExample.java 1.0 29 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

import org.json.simple.*;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.Iterator;

/**
 * Application JsonExample (JsonExample.java)
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 29 Aug 2018
 */
public class JsonExample
{
    public static void main (String[] args)
    {
        writeJson();
        readJson();
    }

    /**
     * Creates JSONObject (apartment) by setting JSON object properties and writes it to file JsonExample/test.json
     *
     * @return JSONObject apartment object
     */
    public static JSONObject writeJson()
    {
        JSONObject obj = new JSONObject();

        obj.put("title", "Luxury 1 Bedroom Condo Downtown");
        obj.put("accommodates", new Integer(2));
        obj.put("bedrooms", new Integer(1));

        JSONArray list = new JSONArray();
        list.add("Kitchen");
        list.add("Internet");
        list.add("TV");

        obj.put("amenities", list);

        // Write JSON object created to file JsonExample/test.json
        try
        {
            FileWriter file = new FileWriter("test.json");
            file.write(obj.toJSONString());
            file.flush();
            file.close();
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }

        return obj;
    }

    /**
     * Reads JSON object from file JsonExample/test.json by parsing each attribute from json to string and prints it
     */
    public static void readJson()
    {
        JSONParser parser = new JSONParser();

        try
        {
            Object obj = parser.parse(new FileReader("test.json"));

            JSONObject jsonObject = (JSONObject) obj;

            String name = (String) jsonObject.get("title");
            System.out.println(name);

            long accommodates = (Long) jsonObject.get("accommodates");
            System.out.println("Accommodates: " + accommodates);

            long bedrooms = (Long) jsonObject.get("bedrooms");
            System.out.println("Bedrooms: " + bedrooms);

            // loop array
            JSONArray msg = (JSONArray) jsonObject.get("amenities");
            System.out.println("Amenities:");
            Iterator<String> iterator = msg.iterator();
            while (iterator.hasNext())
            {
                System.out.println(iterator.next());
            }

        }
        catch (FileNotFoundException e)
        {
            e.printStackTrace();
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }
        catch (ParseException e)
        {
            e.printStackTrace();
        }
    }
}