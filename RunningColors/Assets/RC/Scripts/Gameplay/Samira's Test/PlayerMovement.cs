using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 20;

    Vector3 motion;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        motion = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        rb.velocity = motion * speed;
    }
}
