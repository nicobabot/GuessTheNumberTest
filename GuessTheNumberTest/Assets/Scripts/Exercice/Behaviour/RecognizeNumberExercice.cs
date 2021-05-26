using System.Collections;
using UnityEngine;
using TMPro;

public class RecognizeNumberExercice : MonoBehaviour, IExerciceBehaviour
{
    [Header("Exercice Data")]
    [SerializeField] private int _maxChoices;
    [SerializeField] private int _minRandomRange;
    [SerializeField] private int _maxRandomRange;
    
    [Header("Transitions Data")]
    [SerializeField] private float _timeShowingExerciceNumber = 2;
    [SerializeField] private float _exerciceNumberAlphaDuration= 2;
    [SerializeField] private float _timeShowingAnswer;
    
    [Header("Exercice Objects")]
    [SerializeField] private TextMeshProUGUI _numberWord;
    [SerializeField] private OptionsController _optionsController;
    [SerializeField] private GameObject _exerciceAnswerObject;

    private ExerciceGenerator _exerciceGenerator;
    private TextResourcesManager _textManager;
    private IExerciceAnswer _exerciceAnswer;

    public void Initialize(ExerciceGenerator exerciceGenerator, TextResourcesManager textManager)
    {
        _exerciceGenerator = exerciceGenerator;
        _textManager = textManager;
        _exerciceAnswer = _exerciceAnswerObject.GetComponent<IExerciceAnswer>();
        _optionsController.CreateOptions(_maxChoices);
        _numberWord.alpha = 0;
    }

    public IEnumerator Play()
    {
        Exercice exerciceData = _exerciceGenerator.GenerateNewExercice(_maxChoices, _minRandomRange, _maxRandomRange);
        Debug.Log(exerciceData.correctNumber);
        _numberWord.text = _textManager.NumberToWords(exerciceData.correctNumber);

        yield return ShowExerciceNumber();

        yield return ChooseOptionRoutine(exerciceData);
        
        yield return End();

        StartCoroutine(Play());
    }
    
    private IEnumerator ShowExerciceNumber()
    {
        yield return _numberWord.DoAlphaTransition(1, _exerciceNumberAlphaDuration);
        yield return new WaitForSeconds(_timeShowingExerciceNumber);
        yield return _numberWord.DoAlphaTransition(0, _exerciceNumberAlphaDuration);
    }
    
    private IEnumerator ChooseOptionRoutine(Exercice exerciceData)
    {
        _optionsController.StartExercice(exerciceData, _exerciceAnswer);
        _exerciceAnswer.StartExerciceAnswer(exerciceData, _optionsController);

        yield return new WaitUntil(() => _exerciceAnswer.HasEndExercice);
        yield return new WaitForSeconds(_timeShowingAnswer);

        yield return _optionsController.EndExercice();
    }

    public IEnumerator End()
    {
        yield return _optionsController.EndExercice();
    }
}
