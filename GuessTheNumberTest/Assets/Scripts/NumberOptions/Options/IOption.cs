using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOption
{
    void Initialize();
    void StartExercice(int number, int index, IExerciceAnswer answer);
    IEnumerator Show();
    IEnumerator Hide();
    void SetInteractable(bool state);
    
    //Correct and wrong could be IEnumerators
    void SetCorrect();
    void SetWrong();
}
