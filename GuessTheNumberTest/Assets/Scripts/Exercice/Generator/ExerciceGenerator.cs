using System.Collections.Generic;
using UnityEngine;

public class Exercice
{
    public int correctNumber;
    public List<int> choices = new List<int>();
    public int correctIndexChoice;
}

public class ExerciceGenerator : IExerciceGenerator
{
    public Exercice GenerateNewExercice(int maxChoices, int minRandomRange, int maxRandomRange)
    {
        Exercice exercice = new Exercice();
        exercice.correctNumber = Random.Range(minRandomRange, maxRandomRange);
        exercice.choices = GetListOfChoices(maxChoices, minRandomRange, maxRandomRange, exercice.correctNumber);
        exercice.choices.Shuffle();
        //After the shuffle we store where of the list is the correct choice
        exercice.correctIndexChoice = exercice.choices.IndexOf(exercice.correctNumber);
        return exercice;
    }

    private List<int> GetListOfChoices(int maxChoices, int minRandomRange, int maxRandomRange, int exception)
    {
        int retValue = 0;
        List<int> choices = new List<int>();
        choices.Add(exception);

        //If the number of choices are greater than the range we skip the exercice
        if (!CanGenerateAllChoices(maxChoices, minRandomRange, maxRandomRange))
        {
            return choices;
        }

        for (int i = 0; i < maxChoices - 1; ++i)
        {
            //Keep searching number unitll it's not repeated
            do
            {
                retValue = Random.Range(minRandomRange, maxRandomRange);
            } while (choices.Contains(retValue));

            choices.Add(retValue);
        }

        return choices;
    }

    private bool CanGenerateAllChoices(int maxChoices, int minRandomRange, int maxRandomRange)
    {
        int lengthChoices = Mathf.Abs(maxRandomRange) - Mathf.Abs(minRandomRange);
        return lengthChoices > maxChoices;
    }
}
