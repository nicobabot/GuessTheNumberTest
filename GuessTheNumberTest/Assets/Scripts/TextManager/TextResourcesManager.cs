using System;
using UnityEngine;

[System.Serializable]
public class TextResource
{
    public string[] unitsMap;
    public string[] tensMap;
}

public class TextResourcesManager
{
    public static TextResource TextResources => _textResources;
    private static TextResource _textResources;

    public TextResourcesManager(string textResourcesPath)
    {
        TextAsset temp = Resources.Load(textResourcesPath) as TextAsset;
        _textResources = JsonUtility.FromJson<TextResource>(temp.text);
    }

    public string NumberToWords(int number)
    {
        TextResource _textResources = TextResourcesManager.TextResources;

        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + NumberToWords(Math.Abs(number));

        string words = "";

        words += GetBiggerThanHundredString(number);
        words += GetUnitTensString(number, _textResources);
        return words;
    }

    private string GetBiggerThanHundredString(int number)
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

    private string GetUnitTensString(int number, TextResource _textResources)
    {
        string words = "";
        string[] unitsMap = _textResources.unitsMap;
        string[] tensMap = _textResources.tensMap;

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
