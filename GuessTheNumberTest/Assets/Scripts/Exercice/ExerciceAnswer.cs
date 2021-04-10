using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumbereExercice
{
    public int number;
    public int attempts;
    public int timesFailed;

    public NumbereExercice(int newNumber)
    {
        number = newNumber;
    }

    public void ResetCounter()
    {
        if (timesFailed >= 2)
        {
            timesFailed = 0;
        }
    }
}

public class ExerciceAnswer : MonoBehaviour
{
    public bool HasEndExercice => _hasEndExercice;
    private bool _hasEndExercice;

    private List<NumbereExercice> _numbers = new List<NumbereExercice>();
    private List<NumberOption> _numberOptions;
    private Exercice _exercice;

    private NumbereExercice _numExercice;
    private int _itNum;
    private bool _isNew;

    public void InitializeExercice(Exercice exercice, List<NumberOption> numberOptions)
    {
        _hasEndExercice = false;
        _exercice = exercice;
        _numberOptions = numberOptions;

        int number = _exercice.correctNumber;

        _numExercice = HasPlayedNumber(number, out _itNum);
        _isNew = _numExercice == null;
        if (_isNew)
        {
            _numExercice = new NumbereExercice(number);
        }
    }

    public void NumberChosen(int chosenNumber, int index)
    {
        bool hasGuessed = _numExercice.number == chosenNumber;
        ++_numExercice.attempts;

        if (!hasGuessed)
        {
            FailedExercice();
        }
        else
        {
            SuccessExercice();
        }
    }

    private void FailedExercice()
    {
        ++_numExercice.timesFailed;

        if (_numExercice.timesFailed == 1)
        {
            //First Fail Animation
        }
        else if (_numExercice.timesFailed == 2)
        {
            //Second Fail Animation
        }

        //Send to global counter a fail
        _numExercice.ResetCounter();
        _numbers.Add(_numExercice);
    }

    private void SuccessExercice()
    {
        //Send to global counter a success
        //Success Animation
        _numbers.Add(_numExercice);
    }

    private NumbereExercice HasPlayedNumber(int number, out int it) 
    {
        it = 0;
        foreach (NumbereExercice item in _numbers)
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
