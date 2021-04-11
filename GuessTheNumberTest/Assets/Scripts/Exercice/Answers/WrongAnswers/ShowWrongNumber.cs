using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWrongNumber : WrongAnswers
{
    public override void DoAnswer(int index, OptionsController optionController)
    {
        optionController.ShowFailed(index);
    }
}
