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
        //Vector3 size = GetComponent<Collider>().bounds.size;
        Vector3 size = transform.localScale;
        int pow = (int)Mathf.Pow(2.0f, (float)GameManager.GetInstance().paintResolution + 3);
        int globSize = GameManager.GetInstance().globSize;

        tex = new Texture2D((int)size.x * pow, (int)size.y * pow);

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
            (int)Mathf.Floor(position.x * tex.width),
            (int)Mathf.Floor(position.y * tex.height)
        );

        int scaledGlobSize = GameManager.GetInstance().globSize * (int)(Mathf.Pow(2.0f, (float)GameManager.GetInstance().paintResolution + 3) / 64);

        for (int y = Mathf.Max(pos.y - scaledGlobSize, 0); y < Mathf.Min(pos.y + scaledGlobSize, tex.height); y++)
        {
            for (int x = Mathf.Max(pos.x - scaledGlobSize, 0); x < Mathf.Min(pos.x + scaledGlobSize, tex.width); x++)
            {
                tex.SetPixel(x, y, glob.paintColor);
            }
        }
        tex.Apply();
    }
    public Color GetSurfacePointColor(Vector3 position)
    {
        Vector2Int pos = new(
            (int)Mathf.Floor(position.x * tex.width),
            (int)Mathf.Floor(position.y * tex.height)
        );
        return tex.GetPixel(pos.x, pos.y);
    }
}
