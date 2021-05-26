using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOption
{
    void Initialize();
    void StartExercice(int number, int index, ExerciceAnswer answer);
    IEnumerator Show();
    IEnumerator Hide();
    void SetState(bool state);
    
    //Correct and wrong could be IEnumerators
    void SetCorrect();
    void SetWrong();
}
