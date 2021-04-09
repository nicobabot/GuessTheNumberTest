using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExerciceGame : MonoBehaviour
{
    [Header("Exercice Data")]
    [SerializeField] private int _maxChoices;
    [SerializeField] private int _minRandomRange;
    [SerializeField] private int _maxRandomRange;

    [Header("Exercice Objects")]
    [SerializeField] private Transform _grid;
    [SerializeField] private NumberOption _numberOptionObject;
    [SerializeField] private TextMeshProUGUI _numberWord;


    private List<NumberOption> _numbers = new List<NumberOption>();
    private ExerciceGenerator _exerciceGenerator;

    void Start()
    {
        _exerciceGenerator = new ExerciceGenerator();
        Exercice exerciceData = _exerciceGenerator.GenerateNewExercice(_maxChoices, _minRandomRange, _maxRandomRange);
        _numberWord.text = Constants.NumberToWords(exerciceData.correctNumber);
        for (int i = 0; i < exerciceData.choices.Count; ++i)
        {
            NumberOption nb = Instantiate(_numberOptionObject, _grid);
            nb.Initialize(exerciceData.choices[i]);
            _numbers.Add(nb);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Exercice exerciceData = _exerciceGenerator.GenerateNewExercice(_maxChoices, _minRandomRange, _maxRandomRange);
            Debug.Log(exerciceData.correctNumber);
            Debug.Log(Constants.NumberToWords(exerciceData.correctNumber));
        }
    }
}
