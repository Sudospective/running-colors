using UnityEngine;

[CreateAssetMenu(fileName = "NewScene", menuName = "Scene Data/Scene")]
public class GameSceneSO : ScriptableObject
{
    [Header("Information")]
    public string sceneName;

    [Header("Sounds")]
    public AudioClip music;
}
