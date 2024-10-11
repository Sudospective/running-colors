using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintGlob : MonoBehaviour
{
    [Header("Paint")]
    public Color paintColor;
    public float shotSpeed;
    public int shotSize;

    private Renderer model;
    private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        model = GetComponent<Renderer>();
        model.material.color = paintColor;

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
            Vector3 pos = other.ClosestPoint(transform.position);
            pos.x += other.bounds.size.x * 0.5f;
            paint.PaintSurface(this, pos);
        }
        Destroy(gameObject);
    }
}
