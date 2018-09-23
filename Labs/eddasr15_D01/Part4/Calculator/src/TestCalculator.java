/*
 * @(#)TestCalculator.java 1.0 22 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */

/**
 * TestCalculator application (TestCalculator.java)
 * Tests the Calculator class' functionality
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 22 Aug 2018
 * Licensed under the Open Software License version 3.0
 * (http://opensource.org/licenses/OSL-3.0)
 */
public class TestCalculator
{
    public static void main(String[] args)
    {
        Calculator calc = new Calculator();

        String[] testSet1 =  { "4", "6", "+" };             // Should be 10
        String[] testSet2 =  {  "4", "2", "5", "*",
                                "+", "1", "3", "2",
                                "*", "+", "/" };            // Should be 2
        String[] testSet3 =  {  "5", "1", "2", "+", "4",
                                "*", "3", "-", "+" };       // Should be 14
        String[] testSet4 =  {  "2", "3", "11", "+",
                                "5", "-", "*" };            // Should be 18
        String[] testSet5 =  {  "2", "1", "12", "3",
                                "/", "-", "+" };            // Should be -1
        String[] testSet6 =  { "3", "11", "5", "+", "-"};   // Should be -13
        String[] testSet7 =  {  "3" ,"11" ,"+" ,"5", "-"};  // Should be 9
        String[] testSet8 =  {  "3", "2", "*", "11", "-"};  // Should be -5
        String[] testSet9 =  { "5", "1", "2", "4" };        // Should return minint and print error (invalid expr)
        String[] testSet10 =  { "4", "6", "+", "-" };       // Should return minint and print error (invalid expr)


        System.out.println(calc.evalRPN(testSet1));         // TEST OK
        System.out.println(calc.evalRPN(testSet2));         // TEST OK
        System.out.println(calc.evalRPN(testSet3));         // TEST OK
        System.out.println(calc.evalRPN(testSet4));         // TEST OK
        System.out.println(calc.evalRPN(testSet5));         // TEST OK
        System.out.println(calc.evalRPN(testSet6));         // TEST OK
        System.out.println(calc.evalRPN(testSet7));         // TEST OK
        System.out.println(calc.evalRPN(testSet8));         // TEST OK
        System.out.println(calc.evalRPN(testSet9));         // TEST OK
        System.out.println(calc.evalRPN(testSet10));        // TEST OK
    }
}

