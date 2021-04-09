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

        if ((number / 1000000) > 0)
        {
            words += NumberToWords(number / 1000000) + " million ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " hundred ";
            number %= 100;
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
