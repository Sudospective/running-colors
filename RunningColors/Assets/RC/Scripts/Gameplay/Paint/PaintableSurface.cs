using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintableSurface : MonoBehaviour,
    IPaint
{
    private Texture2D tex;

    // Start is called before the first frame update
    void Start()
    {
        tex = new Texture2D(256, 256);
        Material mat = GetComponent<Renderer>().material;
        mat.mainTexture = tex;
        for (int y = 0; y < tex.height; y++)
        {
            for (int x = 0; x < tex.width; x++)
            {
                tex.SetPixel(x, y, Color.white);
            }
        }
        tex.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PaintSurface(PaintGlob glob, Vector3 position) {
        Vector2Int pos = new(
            (int)Mathf.Floor(position.normalized.x * tex.width),
            (int)Mathf.Floor(position.normalized.y * tex.height)
        );

        for (int y = Mathf.Max(pos.y - glob.shotSize, 0); y < Mathf.Min(pos.y + glob.shotSize, tex.height); y++)
        {
            for (int x = Mathf.Max(pos.x - glob.shotSize, 0); x < Mathf.Min(pos.x + glob.shotSize, tex.width); x++)
            {
                tex.SetPixel(tex.width - x, tex.height - y, glob.paintColor);
            }
        }
        tex.Apply();
    }
}
