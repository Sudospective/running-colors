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
            Debug.Log(pos.ToString());
            pos.x += other.bounds.size.x * 0.5f;
            pos.x /= other.bounds.size.x;
            if (other.gameObject.layer == LayerMask.NameToLayer("whatIsGround")) // Ground
            {
                pos.y = pos.z;
                pos.y += other.bounds.size.z * 0.5f;
                pos.y /= other.bounds.size.z;
            }
            else
            {
                pos.y /= other.bounds.size.y;
            }
            Debug.Log(pos.ToString());
            paint.PaintSurface(this, pos);
        }
        Destroy(gameObject);
    }
}
