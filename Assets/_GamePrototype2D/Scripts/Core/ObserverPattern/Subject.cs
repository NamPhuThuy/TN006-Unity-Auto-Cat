using System.Collections.Generic;
using UnityEngine;

//This class is being observed by IObserver
public abstract class Subject : MonoBehaviour 
{
    // a collection of all the observers pattern of this subject
    // Lists & HashSets provide simplest setup
    // Dictionaries provides better performance for large collections
    private List<IObserver> _observers = new List<IObserver>();


    //Add one more object to observe this subject

    public void AddObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers(string action)
    {
        _observers.ForEach((_observer) =>
        {
            _observer.OnNotify(action);
        });
    }

    public void NotifyObservers(IGameEvent e)
    {
        _observers.ForEach((_observer) =>
        {
            _observer.OnNotify(e);
        });
    }

    public void NotifyObservers()
    {
        _observers.ForEach((_observer) =>
        {
            _observer.OnNotify();
        });
    }
}