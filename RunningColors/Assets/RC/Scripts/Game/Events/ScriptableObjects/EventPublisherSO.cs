using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This is a base class for publishers.
/// </summary>

[CreateAssetMenu(menuName = "Events/Event Publisher")]
public class EventPublisherSO : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
    {
       // The publisher determines when an event is
       // raised and broadcasts to registered listners
       OnEventRaised?.Invoke();
    }
}
