using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "WinEventPublisher", menuName = "Events/Win Event Publisher")]
public class WinEventPublisherSO : ScriptableObject
{
    public UnityAction OnLevelComplete;

    public void RaiseEvent()
    {
        Debug.Log("Player completed the level");
        OnLevelComplete?.Invoke();
    }
}
