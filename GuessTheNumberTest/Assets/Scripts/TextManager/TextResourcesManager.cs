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
    public int noPatternsNumber;
    public string oneMillionValue;
}

public class TextResourcesManager
{
    public static TextResource TextResources => _textResources;
    private static TextResource _textResources;

    //We will get the text from a JSON in case we want to change lenaguage
    public TextResourcesManager(string textResourcesPath)
    {
        TextAsset temp = Resources.Load(textResourcesPath) as TextAsset;
        _textResources = JsonUtility.FromJson<TextResource>(temp.text);
    }

    public string NumberToWords(int number)
    {
        //Zero case
        if (number == 0)
            return _textResources.unitsMap.GetValueOrDefault(0);

        //In case is negative
        if (number < 0)
            return _textResources.minusValue + " " + NumberToWords(Math.Abs(number));

        string words = "";
        words = GetBiggerThanHundredString(number);
        return words;
    }

    //We will keep getting Million/Thousand/Hundred rest to continue with the number word
    private string GetBiggerThanHundredString(int number)
    {
        string words = "";

        int millionValue = 1000000;
        if ((number / millionValue) > 0)
        {
            int value = number / millionValue;
            //Spanish has first number exceptions :/
            if (value != 1)
            {
                words += NumberToWords(value) + " " + _textResources.millionValue + " ";
            }
            else
            {
                words += _textResources.oneMillionValue + " ";
            }
            number %= millionValue;
        }

        int thousandValue = 1000;
        if ((number / thousandValue) > 0)
        {
            int value = number / thousandValue;
            //More first number exceptions
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
            //We have to many exceptions in hundred values so we get them from JSON
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

            //Less than X number there is no pattern
            if (number < _textResources.noPatternsNumber)
                words += unitsMap.GetValueOrDefault(number);
            else
            {
                //We get the tens
                words += tensMap.GetValueOrDefault(number / 10);
                //And add the units
                if ((number % 10) > 0)
                    words += " "+ _textResources.andValue + " " + unitsMap.GetValueOrDefault(number % 10);
            }
        }

        return words;
    }
}
