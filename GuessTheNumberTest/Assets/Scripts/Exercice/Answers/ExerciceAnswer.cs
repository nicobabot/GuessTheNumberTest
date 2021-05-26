using System.Collections.Generic;
using UnityEngine;

public class NumberExercice
{
    public int number;
    public int attempts;
    public int timesFailed;

    //Value to reset exercice answer animation
    private int _resetCounterValue = 0;

    public NumberExercice(int newNumber, int resetCounterValue)
    {
        number = newNumber;
        _resetCounterValue = resetCounterValue;
    }

    public void ResetCounter(bool force = false)
    {
        if (timesFailed >= _resetCounterValue || force)
        {
            timesFailed = 0;
        }
    }
}

public class ExerciceAnswer : MonoBehaviour, IExerciceAnswer
{
    [SerializeField] private int _failResetCounter = 2;
    [SerializeField] private WrongAnswers[] _wrongAnswers;

    public bool HasEndExercice => _hasEndExercice;
    private bool _hasEndExercice;

    private List<NumberExercice> _numbers = new List<NumberExercice>();
    private OptionsController _optionsController;
    private Exercice _exercice;
    private NumberExercice _numExercice;


    //Events to warn of an Exercice Answer
    public delegate void CorrectAnswer();
    public static event CorrectAnswer onCorrectAnswer;

    public delegate void WrongAnswer();
    public static event WrongAnswer onWrongAnswer;

    public void StartExerciceAnswer(Exercice exercice, OptionsController optionsController)
    {
        _hasEndExercice = false;
        _exercice = exercice;
        _optionsController = optionsController;

        //If we already used the number or we need to create it
        int number = _exercice.correctNumber;
        _numExercice = HasPlayedNumber(number);
        if (_numExercice == null)
        {
            _numExercice = new NumberExercice(number, _failResetCounter);
        }
    }

    public void ChoseNumber(int chosenNumber, int index)
    {
        _optionsController.SetOptionsState(false);
        bool hasGuessed = _numExercice.number == chosenNumber;
        ++_numExercice.attempts;

        if (hasGuessed)
        {
            SuccessExercice();
        }
        else
        {
            FailedExercice(index);
        }
        _hasEndExercice = true;
    }

    private void FailedExercice(int index)
    {
        ++_numExercice.timesFailed;

        //Get the correct answer related to times failed this number
        WrongAnswers answer = GetWrongAnswer(_numExercice.timesFailed);
        if (answer != null)
        {
            answer.DoAnswer(index, _optionsController);
        }

        _numExercice.ResetCounter();
        _numbers.Add(_numExercice);

        //Send to global event fail
        onWrongAnswer?.Invoke();
    }

    private void SuccessExercice()
    {
        //Success Animation
        _optionsController.ShowCorrect();

        //Correct answer -> Force reset error counter
        _numExercice.ResetCounter(true);
        _numbers.Add(_numExercice);

        //Send to global event success
        onCorrectAnswer?.Invoke();
    }

    private NumberExercice HasPlayedNumber(int number) 
    {
        foreach (NumberExercice item in _numbers)
        {
            if (item.number == number)
            {
                return item;
            }
        }
        return null;
    }

    private WrongAnswers GetWrongAnswer(int failIndex)
    {
        foreach (WrongAnswers item in _wrongAnswers)
        {
            if (item.FailCounterIndex == failIndex)
            {
                return item;
            }
        }
        return null;
    }
}
