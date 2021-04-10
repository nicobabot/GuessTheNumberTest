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
        for (int i = 0; i < _maxChoices; ++i)
        {
            NumberOption nb = Instantiate(_numberOptionObject, _grid);
            _numbers.Add(nb);
        }
        _numberWord.alpha = 0;
        StartCoroutine(PlayExercice());
    }

    private IEnumerator PlayExercice()
    {
        Exercice exerciceData = _exerciceGenerator.GenerateNewExercice(_maxChoices, _minRandomRange, _maxRandomRange);
        _numberWord.text = Constants.NumberToWords(exerciceData.correctNumber);


        yield return _numberWord.DoAlphaTransition(1, 2);
        yield return new WaitForSeconds(2);
        yield return _numberWord.DoAlphaTransition(0, 2);


        for (int i = 0; i < exerciceData.choices.Count; ++i)
        {
            NumberOption nb = _numbers.GetValueOrDefault(i);
            if (nb != null)
            {
                nb.Initialize(exerciceData.choices[i]);
                StartCoroutine(nb.ShowOption());
            }
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
