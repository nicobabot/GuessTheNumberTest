using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBothNumber : WrongAnswers
{
    public override void DoAnswer(int index, OptionsController optionController)
    {
        optionController.ShowFailedAndCorrect(index);
    }
}
