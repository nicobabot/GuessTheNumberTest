using System;
using UnityEngine;

[System.Serializable]
public class TextResource
{
    public string[] unitsMap;
    public string[] tensMap;
    public string[] hundredMap;
    public string thousandValue;
    public string millionValue;
    public string andValue;
    public string minusValue;
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
            return _textResources.minusValue + " " + NumberToWords(Math.Abs(number));

        string words = "";
        words = GetBiggerThanHundredString(number);
        return words;
    }

    private string GetBiggerThanHundredString(int number)
    {
        string words = "";

        int millionValue = 1000000;
        if ((number / millionValue) > 0)
        {
            int value = number / millionValue;
            words += NumberToWords(value) + " " + _textResources.millionValue + " ";
            number %= millionValue;
        }

        int thousandValue = 1000;
        if ((number / thousandValue) > 0)
        {
            int value = number / thousandValue;
            if (value != 1)
            {
                words += NumberToWords(value) + " ";
            }
            words += _textResources.thousandValue + " ";
            number %= thousandValue;
        }

        int hundredValue = 100;
        if ((number / hundredValue) > 0)
        {
            int value = number / hundredValue;
            words += _textResources.hundredMap.GetValueOrDefault(value) + " ";        
            number %= hundredValue;
        }

        words += GetUnitTensString(number);

        return words;
    }

    private string GetUnitTensString(int number)
    {
        string words = "";
        string[] unitsMap = _textResources.unitsMap;
        string[] tensMap = _textResources.tensMap;

        if (number > 0)
        {
            if (words != "")
                words += _textResources.andValue + " ";

            if (number < 30)
                words += unitsMap.GetValueOrDefault(number);
            else
            {
                words += tensMap.GetValueOrDefault(number / 10);
                if ((number % 10) > 0)
                    words += " "+ _textResources.andValue + " " + unitsMap.GetValueOrDefault(number % 10);
            }
        }

        return words;
    }
}
