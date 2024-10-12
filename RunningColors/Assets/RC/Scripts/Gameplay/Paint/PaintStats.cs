using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PaintType
{
    None,
    Speed,
    Jump,
    Stick,
}

[CreateAssetMenu]

public class PaintStats : ScriptableObject
{
    // Color
    public PaintType type;
    public Color color;
}
