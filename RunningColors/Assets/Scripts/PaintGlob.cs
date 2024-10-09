using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintGlob : MonoBehaviour
{

    public enum PaintColor
    {
        Green, Blue, Purple
    }

    [Header("Paint")]
    public PaintColor paintColor;
    public float shotSpeed;

    private Renderer model;
    private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        model = GetComponent<Renderer>();
        Color col = Color.white;
        switch (paintColor) {
            case PaintColor.Green:
                col = Color.green;
                break;
            case PaintColor.Blue:
                col = Color.blue;
                break;
            case PaintColor.Purple:
                col = new Color(1.0f, 0.0f, 1.0f, 1.0f);
                break;
        }
        model.material.color = col;

        body = GetComponent<Rigidbody>();
        body.velocity = GameManager.GetInstance().player.GetComponent<Rigidbody>().velocity + (transform.forward * shotSpeed);
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            return;
        }
        IPaint paint = other.GetComponent<IPaint>();
        if (paint != null)
        {
            paint.PaintSurface();
        }
        Destroy(gameObject);
    }
}
