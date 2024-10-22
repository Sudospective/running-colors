using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFollow : MonoBehaviour
{
    public Transform player; 
    public float shadowHeight = 0.1f;
    public LayerMask groundLayer;

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 playerPosition = player.position;

        RaycastHit hit;
        if (Physics.Raycast(playerPosition, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            transform.position = new Vector3(playerPosition.x, hit.point.y + shadowHeight, playerPosition.z);
        }
    }
}