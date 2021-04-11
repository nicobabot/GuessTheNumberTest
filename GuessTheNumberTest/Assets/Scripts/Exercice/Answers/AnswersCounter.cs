﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswersCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _correctAnswersText;
    [SerializeField] private TextMeshProUGUI _incorrectAnswersText;

    private int _correctAnswers;
    private int _incorrectAnswers;

    private void OnEnable()
    {
        ExerciceAnswer.onIncorrectAnswer += AddIncorrectCounter;
        ExerciceAnswer.onCorrectAnswer += AddCorrectCounter;
    }

    private void OnDisable()
    {
        ExerciceAnswer.onIncorrectAnswer -= AddIncorrectCounter;
        ExerciceAnswer.onCorrectAnswer -= AddCorrectCounter;
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
