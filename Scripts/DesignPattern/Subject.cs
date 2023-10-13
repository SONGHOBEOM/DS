using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private List<IObserver> observers = new List<IObserver>();

    public void RegisterObserver(IObserver observer)
    {
        if(!(observers.Contains(observer))) observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        if(observers.Contains(observer)) observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach(IObserver observer in observers)
        {
            observer.Notify(this);
        }
    }
}
