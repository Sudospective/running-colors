using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class PaintStats : ScriptableObject
{
    public enum PaintType
    {
        Speed,
        Jump,
        Stick,
    }
    // Color
    public PaintType type;
    public Color color;
}
