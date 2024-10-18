using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Listens for the event to be raised by the publisher, then responds to it. Responses can be connected directly from the Unity Inspector.
/// </summary>
public class EventListener : MonoBehaviour
{
    [Tooltip("The publisher that will raise the event when it occurs")]
    [SerializeField] EventPublisherSO _publisher = default;

    public UnityEvent OnEventRaised;

    private void OnEnable()
    {
        // Adds a subscriber to the event;
        // Can have more than one subscriber
        _publisher.OnEventRaised += Respond;
    }

    private void OnDisable()
    {
        // Removes a subscriber from the event
        _publisher.OnEventRaised -= Respond;
    }

    void Respond()
    {
        // If the event is raised, invoke all subscribers synchronously
        OnEventRaised?.Invoke();
    }
}
