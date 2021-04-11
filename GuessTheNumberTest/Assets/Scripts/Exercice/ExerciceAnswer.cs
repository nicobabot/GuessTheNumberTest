using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberExercice
{
    public int number;
    public int attempts;
    public int timesFailed;

    public NumberExercice(int newNumber)
    {
        number = newNumber;
    }

    public void ResetCounter(bool force = false)
    {
        if (timesFailed >= 2 || force)
        {
            timesFailed = 0;
        }
    }
}

public class ExerciceAnswer : MonoBehaviour
{
    public bool HasEndExercice => _hasEndExercice;
    private bool _hasEndExercice;

    private List<NumberExercice> _numbers = new List<NumberExercice>();
    private List<NumberOption> _numberOptions;
    private Exercice _exercice;

    private NumberExercice _numExercice;
    private int _itNum;
    private bool _isNew;

    public void StartExerciceAnswer(Exercice exercice, List<NumberOption> numberOptions)
    {
        _hasEndExercice = false;
        _exercice = exercice;
        _numberOptions = numberOptions;

        int number = _exercice.correctNumber;

        _numExercice = HasPlayedNumber(number, out _itNum);
        _isNew = _numExercice == null;
        if (_isNew)
        {
            _numExercice = new NumberExercice(number);
        }
    }

    public void NumberChosen(int chosenNumber, int index)
    {
        _numberOptions.SetState(false);
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
    }

    private void FailedExercice(int index)
    {
        ++_numExercice.timesFailed;

        NumberOption number = _numberOptions.GetValueOrDefault(index);

        if (_numExercice.timesFailed == 1)
        {
            //First Fail Animation
            if (number != null)
            {
                number.SetWrongOption();
            }
        }
        else if (_numExercice.timesFailed == 2)
        {
            //Second Fail Animation
            if (number != null)
            {
                number.SetWrongOption();
            }
            NumberOption correctNumber = _numberOptions.GetValueOrDefault(_exercice.correctIndexChoice);
            if (correctNumber != null)
            {
                correctNumber.SetCorrectOption();
            }
        }
        //Send to global counter a fail
        _numExercice.ResetCounter();
        _numbers.Add(_numExercice);
        _hasEndExercice = true;
    }

    private void SuccessExercice()
    {
        //Send to global counter a success
        //Success Animation
        NumberOption number = _numberOptions.GetValueOrDefault(_exercice.correctIndexChoice);
        if (number != null)
        {
            number.SetCorrectOption();
        }
        //We force to reset the fail counter because the user has been successful
        _numExercice.ResetCounter(true);
        _numbers.Add(_numExercice);
        _hasEndExercice = true;
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
