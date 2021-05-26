using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSubject : MonoBehaviour
{
    public void AddListener(AbstactObserver observer)
    {
        if (HasObserver(observer) == false)
        {
            _observers.Add(observer);
        }
    }

    public void RemoveListener(AbstactObserver observer)
    {
        if (HasObserver(observer))
        {
            _observers.Remove(observer);
        }
    }

    protected void Notify()
    {
        foreach (AbstactObserver observer in _observers)
        {
            observer.UpdateObserver(this);
        }
    }

    private bool HasObserver(AbstactObserver observer)
    {
        return _observers.Contains(observer);
    }
    
    private List<AbstactObserver> _observers = new List<AbstactObserver>();
}
