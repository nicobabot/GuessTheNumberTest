using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciceGame : MonoBehaviour
{
    [Header("Exercice Data")]
    [SerializeField] private int _maxChoices;
    [SerializeField] private int _minRandomRange;
    [SerializeField] private int _maxRandomRange;

    private ExerciceGenerator _exerciceGenerator;

    void Start()
    {
        _exerciceGenerator = new ExerciceGenerator();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Exercice exerciceData = _exerciceGenerator.GenerateNewExercice(_maxChoices, _minRandomRange, _maxRandomRange); ;
            Debug.Log(exerciceData.correctNumber);
            Debug.Log(Constants.NumberToWords(exerciceData.correctNumber));
        }
    }
}
