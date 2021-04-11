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

public class ExerciceAnswer : MonoBehaviour
{
    [SerializeField] private int _failResetCounter = 2;
    public bool HasEndExercice => _hasEndExercice;
    private bool _hasEndExercice;

    private List<NumberExercice> _numbers = new List<NumberExercice>();
    private OptionsController _optionsController;
    private Exercice _exercice;

    private NumberExercice _numExercice;
    private int _itNum;
    private bool _isNew;

    public delegate void CorrectAnswer();
    public static event CorrectAnswer onCorrectAnswer;

    public delegate void IncorrectAnswer();
    public static event IncorrectAnswer onIncorrectAnswer;

    public void StartExerciceAnswer(Exercice exercice, OptionsController optionsController)
    {
        _hasEndExercice = false;
        _exercice = exercice;
        _optionsController = optionsController;

        //If we already used the number or we need to create it
        int number = _exercice.correctNumber;
        _numExercice = HasPlayedNumber(number, out _itNum);
        _isNew = _numExercice == null;
        if (_isNew)
        {
            _numExercice = new NumberExercice(number, _failResetCounter);
        }
    }

    public void NumberChosen(int chosenNumber, int index)
    {
        _optionsController.SetOptionsState(false);
        bool hasGuessed = _numExercice.number == chosenNumber;
        ++_numExercice.attempts;

        if (!hasGuessed)
        {
            FailedExercice(index);
        }
        else
        {
            SuccessExercice();
        }
        _hasEndExercice = true;
    }

    private void FailedExercice(int index)
    {
        ++_numExercice.timesFailed;

        if (_numExercice.timesFailed == 1)
        {
            //First Fail Animation
            _optionsController.ShowFailed(index);
        }
        else if (_numExercice.timesFailed == 2)
        {
            //Second Fail Animation
            _optionsController.ShowFailedAndCorrect(index);
        }

        _numExercice.ResetCounter();
        _numbers.Add(_numExercice);

        //Send to global event fail
        onIncorrectAnswer?.Invoke();
    }

    private void SuccessExercice()
    {
        //Success Animation
        _optionsController.ShowCorrect();

        //Correct answer -> Force reset counter
        _numExercice.ResetCounter(true);
        _numbers.Add(_numExercice);

        //Send to global event success
        onCorrectAnswer?.Invoke();
    }

    private NumberExercice HasPlayedNumber(int number, out int it) 
    {
        it = 0;
        foreach (NumberExercice item in _numbers)
        {
            if (item.number == number)
            {
                return item;
            }
            ++it;
        }
        return null;
    }
}
