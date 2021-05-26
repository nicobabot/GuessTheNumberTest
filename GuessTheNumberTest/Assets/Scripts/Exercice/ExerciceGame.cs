using System.Collections;
using UnityEngine;
using TMPro;

public class ExerciceGame : MonoBehaviour
{
    [SerializeField] private GameObject _exerciceObject;

    [Header("Text Data")]
    [SerializeField] private string _textDocument;

    private ExerciceGenerator _exerciceGenerator;
    private TextResourcesManager _textManager;
    private IExerciceBehaviour _exercice;

    void Start()
    {
        _textManager = new TextResourcesManager(_textDocument);
        _exerciceGenerator = new ExerciceGenerator();
        _exercice = _exerciceObject.GetComponent<IExerciceBehaviour>();
        _exercice.Initialize(_exerciceGenerator, _textManager);
        StartCoroutine(_exercice.Play());
    }
}
