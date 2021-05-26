using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstactObserver: MonoBehaviour
{
    public abstract void UpdateObserver(AbstractSubject subject);
}
