using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public Transform trackedObject;

    public float moveSpeed = 20;
    public float updateSpeed = 10;

    [Range(0, 10)] public float currentDistance = 5;
    public float maxDistance = 10;
    public float hideDistance = 1.5f;

    string moveAxis = "Mouse ScrollWheel";

    GameObject ahead;

    //MeshRenderer renderer;

    void Start()
    {
        ahead = new GameObject("ahead");
        //renderer = trackedObject.gameObject.GetComponent<MeshRenderer>();
    }

    void LateUpdate()
    {
        ahead.transform.position = trackedObject.position + transform.forward * (maxDistance * 0.25f);

        currentDistance += Input.GetAxisRaw(moveAxis) * moveSpeed * Time.deltaTime;
        currentDistance = Mathf.Clamp(currentDistance, 0, maxDistance);

        transform.position = Vector3.MoveTowards(transform.position, trackedObject.position + Vector3.up * currentDistance 
            - trackedObject.forward * (currentDistance + maxDistance * 0.5f), updateSpeed * Time.deltaTime);

        transform.LookAt(ahead.transform);

        //renderer.enabled = (currentDistance > hideDistance);
    }
}
