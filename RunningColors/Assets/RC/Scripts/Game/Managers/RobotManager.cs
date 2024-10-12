using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour
{
    public Transform[] waypoints;
    [SerializeField] float speed = 3f;
    [SerializeField] float stoppingDist = 0.2f;

    private int currentWaypointIndex = 0;
    

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        //find current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        //move towards current waypoint while ignoreing y axis
        Vector3 dir = new Vector3 (targetWaypoint.position.x - transform.position.x, 0, targetWaypoint.position.z - transform.position.z).normalized;
        Debug.Log($"Direction to wapoint {currentWaypointIndex}: {dir}");
        transform.position += dir * speed * Time.deltaTime;

        //checks if robot reaches waypoint
        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                            new Vector3(targetWaypoint.position.x, 0, targetWaypoint.position.z)) < stoppingDist)
        {
            //switches to next waypoint
            Debug.Log("Switching to next waypoint");
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        //rotates robot towards waypoint
        if (dir != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotate, Time.deltaTime * speed);
        }
    }
}
