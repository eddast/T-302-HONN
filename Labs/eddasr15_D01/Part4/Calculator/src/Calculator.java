/*
 * @(#)Calculator.java 1.0 22 Aug 2018 Edda Steinunn Rúnarsdóttir
 *
 * Copyright (c) Edda Steinunn Rúnarsdóttir
 */
import java.util.Stack;

/**
 * Calculator class (Calculator.java)
 * Has method EvalRPN which computes the results from a
 * reverse polish notation expression
 *
 * @author Edda Steinunn Rúnarsdóttir
 * @version 1.0, 22 Aug 2018
 * Licensed under the Open Software License version 3.0
 * (http://opensource.org/licenses/OSL-3.0)
 */
public class Calculator
{

    /**
     * Evaluates RPN-expression
     *
     * @param tokens ordered string array of values and tokens that represent the RPN-expression to be calculated
     *
     * @return result of RPN expression, if RPN expression was invalid min integer is returned and error is printed
     */
    public int evalRPN(String[] tokens)
    {
        try
        {
            return parseExpression(tokens);
        }
        catch (Exception e)
        {
            e.printStackTrace();
            return Integer.MIN_VALUE;
        }
    }

    /**
     * Segregates operators and values from RPN-expression into two stacks and calculates it
     *
     * @param RPNExpression ordered string array of values and tokens that represent the RPN-expression to be calculated
     *
     * @return result of expression
     *
     * @throws Exception if RPN expression is invalid
     */
    private int parseExpression(String[] RPNExpression) throws Exception
    {
        // value stack set for numeric values
        Stack<Integer> values = new Stack<Integer>();

        for(int i = 0; i < RPNExpression.length; i++)
        {
            // if token is numeric value it is pushed onto value stack
            if(isValue(RPNExpression[i]))
            {
                values.push(Integer.parseInt(RPNExpression[i]));
            }
            else
            {
                String token = RPNExpression[i];

                // any non-value non-operator token is considered to invalidate RPN expression and raises error
                if(!isOperator(token))
                {
                    throw new Exception("Invalid token passed into evalRPN function");
                }

                // any operator token requires correct evaluation of two top-of-stack values in respect to operator
                if(values.size() < 2)
                {
                    throw new Exception("Invalid RPN expression passed into evalRPN");
                }
                int rhs = values.pop();
                int lhs = values.pop();
                switch(token)
                {
                    case "+" :
                        values.push((lhs+rhs));
                        break;
                    case "-":
                        values.push((lhs-rhs));
                        break;
                    case "*":
                        values.push((lhs*rhs));
                        break;
                    case "/":
                        values.push((lhs/rhs));
                }
            }
        }

        int result = values.pop();

        // If values and tokens have not been emptied RPN expression is wrongly formatted
        if(!values.isEmpty() && RPNExpression.length != 0)
        {
            throw new Exception("Invalid expression passed into evalRPN function");
        }

        return result;
    }

    /**
     * @param token token to determine whether is an operator
     *
     * @return true if string is a valid operator (recognized operators; +, -, * and /
     */
    private boolean isOperator(String token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/";
    }

    /**
     * @param value String to determine whether is a value (numeric) as opposed to an operator or other token
     *
     * @return true if string is a numeric value
     */
    private boolean isValue(String value)
    {
        if(value == "-")
        {
            return false;
        }
        for(int i = 0; i < value.length(); i++)
        {
            if( i == 0 && value.charAt(i) == '-')
            {
                continue;
            }
            if( !Character.isDigit(value.charAt(i)) )
            {
                return false;
            }
        }
        return true;
    }
}
