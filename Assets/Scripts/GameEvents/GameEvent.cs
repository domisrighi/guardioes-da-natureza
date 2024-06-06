using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> Listeners = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = Listeners.Count - 1; i >= 0; i--)
        {
            Listeners[i].OnEventRaised();
        }
    }

    public void Register(GameEventListener listener)
    {
        if (!Listeners.Contains(listener))
        {
            Listeners.Add(listener);
        }
    }
    public void UnRegister(GameEventListener listener)
    {
        if (Listeners.Contains(listener))
        {
            Listeners.Remove(listener);
        }
    }
}
