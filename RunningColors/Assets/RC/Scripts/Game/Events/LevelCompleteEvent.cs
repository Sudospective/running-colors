using UnityEngine;

public class LevelCompleteEvent : MonoBehaviour
{
    [SerializeField] WinEventPublisherSO winEventPublisher;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (winEventPublisher != null)
                winEventPublisher.RaiseEvent();
        }
    }
}
