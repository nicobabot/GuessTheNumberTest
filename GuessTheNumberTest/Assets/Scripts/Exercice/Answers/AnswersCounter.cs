using UnityEngine;
using TMPro;

public class AnswersCounter : AbstactObserver
{
    [SerializeField] private AbstractSubject _subject;
    [SerializeField] private TextMeshProUGUI _correctAnswersText;
    [SerializeField] private TextMeshProUGUI _incorrectAnswersText;

    private int _correctAnswers;
    private int _incorrectAnswers;

    private void OnEnable()
    {
        _subject?.AddListener(this);
    }

    private void OnDisable()
    {
        _subject?.RemoveListener(this);
    }
    
    public override void UpdateObserver(AbstractSubject subject)
    {
        IExerciceAnswer answer = subject as IExerciceAnswer;

        if (answer != null)
        {
            if (answer.IsCorrectAnswer)
            {
                AddCorrectCounter();
            }
            else
            {
                AddIncorrectCounter();
            }
        }
    }
    
    private void AddCorrectCounter()
    {
        ++_correctAnswers;
        _correctAnswersText.text = _correctAnswers.ToString();
    }

    private void AddIncorrectCounter()
    {
        ++_incorrectAnswers;
        _incorrectAnswersText.text = _incorrectAnswers.ToString();
    }
}
