using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPaint
{
    public void PaintSurface(PaintGlob glob, Vector3 position);
    public PaintType GetSurfacePaintType(Vector3 position);
}
