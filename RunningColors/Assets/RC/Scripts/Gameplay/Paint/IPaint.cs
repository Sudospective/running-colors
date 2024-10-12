using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPaint
{
    public void PaintSurface(PaintGlob glob, Vector3 position);
    public Color GetSurfacePointColor(Vector3 position);
}
