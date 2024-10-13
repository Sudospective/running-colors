using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintableSurface : MonoBehaviour,
    IPaint
{
    private Texture2D tex;
    private PaintType[,] paintTypes;

    // Start is called before the first frame update
    void Start()
    {
        //Vector3 size = GetComponent<Collider>().bounds.size;
        Vector3 size = transform.localScale;
        int pow = (int)Mathf.Pow(2.0f, (float)GameManager.GetInstance().paintResolution + 3);
        int globSize = GameManager.GetInstance().globSize;

        tex = new Texture2D((int)size.x * pow, (int)size.y * pow);
        paintTypes = new PaintType[tex.width, tex.height];

        Material mat = GetComponent<Renderer>().material;
        mat.mainTexture = tex;

        for (int y = 0; y < tex.height; y++)
        {
            for (int x = 0; x < tex.width; x++)
            {
                tex.SetPixel(x, y, Color.white);
                paintTypes[x, y] = PaintType.None;
            }
        }
        tex.Apply();
    }

    public void PaintSurface(PaintGlob glob, Vector3 position)
    {
        // Convert world space to texture position
        position = transform.InverseTransformPoint(position);
        position += new Vector3(0.5f, 0.5f);
        Vector2Int pos = new(
            (int)Mathf.Floor(position.x * tex.width),
            (int)Mathf.Floor(position.y * tex.height)
        );

        int scaledGlobSize = GameManager.GetInstance().globSize * (int)(Mathf.Pow(2.0f, (float)GameManager.GetInstance().paintResolution + 3) / 64);

        // Painting the surface
        for (int y = Mathf.Max(pos.y - scaledGlobSize, 0); y < Mathf.Min(pos.y + scaledGlobSize, tex.height); y++)
        {
            for (int x = Mathf.Max(pos.x - scaledGlobSize, 0); x < Mathf.Min(pos.x + scaledGlobSize, tex.width); x++)
            {
                tex.SetPixel(x, y, glob.paintColor);
                paintTypes[x, y] = glob.paintType;
            }
        }
        tex.Apply();
    }
    public PaintType GetSurfacePaintType(Vector3 position)
    {
        // Convert world space to texture position
        position = transform.InverseTransformPoint(position);
        position += new Vector3(0.5f, 0.5f);
        Vector2Int pos = new(
            (int)Mathf.Floor(position.x * tex.width),
            (int)Mathf.Floor(position.y * tex.height)
        );

        return paintTypes[pos.x, pos.y];
    }
}
