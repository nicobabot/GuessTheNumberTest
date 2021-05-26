using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExerciceBehaviour
{
    void Initialize(ExerciceGenerator exerciceGenerator, TextResourcesManager textManager);
    IEnumerator Play();
    IEnumerator End();
}
