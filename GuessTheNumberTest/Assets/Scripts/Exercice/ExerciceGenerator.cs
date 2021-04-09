using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercice
{
    public int correctNumber;
    public List<int> choices = new List<int>();
}

public class ExerciceGenerator
{
    public Exercice GenerateNewExercice(int maxChoices, int minRandomRange, int maxRandomRange)
    {
        Exercice exercice = new Exercice();
        exercice.correctNumber = Random.Range(minRandomRange, maxRandomRange);
        exercice.choices = GetListOfChoices(maxChoices, minRandomRange, maxRandomRange, exercice.correctNumber);
        exercice.choices.Shuffle();
        return exercice;
    }

    private List<int> GetListOfChoices(int maxChoices, int minRandomRange, int maxRandomRange, int exception)
    {
        int retValue = 0;
        List<int> choices = new List<int>();
        choices.Add(exception);

        for (int i = 0; i < maxChoices - 1; ++i)
        {
            do
            {
                retValue = Random.Range(minRandomRange, maxRandomRange);
            } while (choices.Contains(retValue));

            choices.Add(retValue);
        }

        return choices;
    }

}
