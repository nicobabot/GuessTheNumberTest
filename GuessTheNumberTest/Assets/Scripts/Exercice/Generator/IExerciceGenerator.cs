using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExerciceGenerator
{
    Exercice GenerateNewExercice(int maxChoices, int minRandomRange, int maxRandomRange);
}
