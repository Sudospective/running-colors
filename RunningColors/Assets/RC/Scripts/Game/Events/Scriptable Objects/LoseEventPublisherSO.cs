using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "LoseEventPublisher", menuName = "Events/Lose Event Publisher")]
public class LoseEventPublisherSO : ScriptableObject
{
    public UnityAction OnLose;

    public void RaiseEvent()
    {
        Debug.Log("Player lost");
        OnLose?.Invoke();
    }
}
