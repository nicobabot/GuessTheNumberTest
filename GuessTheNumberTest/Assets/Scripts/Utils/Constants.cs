using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    static string[] unitsMap = new string [] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", 
                "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

    static string[] tensMap = new string[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
    public static string NumberToWords(int number)
    {
        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + NumberToWords(Math.Abs(number));

        string words = "";

        words += GetBiggerThanHundredString(number);
        words += GetUnitTensString(number);
        return words;
    }

    private static string GetBiggerThanHundredString(int number)
    {
        string words = "";

        int millionValue = 1000000;
        if ((number / millionValue) > 0)
        {
            words += NumberToWords(number / millionValue) + " million ";
            number %= millionValue;
        }

        int thousandValue = 1000;
        if ((number / thousandValue) > 0)
        {
            words += NumberToWords(number / thousandValue) + " thousand ";
            number %= thousandValue;
        }

        int hundredValue = 100;
        if ((number / hundredValue) > 0)
        {
            words += NumberToWords(number / hundredValue) + " hundred ";
            number %= hundredValue;
        }

        return words;
    }

    private static string GetUnitTensString(int number)
    {
        string words = "";

        if (number > 0)
        {
            if (words != "")
                words += "and ";


            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words;
    }
}
