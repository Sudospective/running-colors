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
            float radX = other.transform.rotation.eulerAngles.x * Mathf.Deg2Rad;
            float radY = other.transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
            Vector3 scale = new Vector3(
                1 + Mathf.Abs(Mathf.Tan(radY)),
                1 + Mathf.Abs(Mathf.Tan(radX)),
                1
            );
            pos.Scale(scale);
            pos += other.transform.localScale * 0.5f;
            pos -= Vector3.Scale(other.transform.position, scale);
            pos.x /= other.transform.localScale.x;
            pos.y /= other.transform.localScale.y;
            pos.z /= other.transform.localScale.z;
            paint.PaintSurface(this, pos);
        }
        Destroy(gameObject);
    }
}
