using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WrongAnswers : MonoBehaviour
{
    public int FailCounterIndex => _failCounterIndex;
    [SerializeField] private int _failCounterIndex;

    public abstract void DoAnswer(int index, OptionsController optionController);
}
