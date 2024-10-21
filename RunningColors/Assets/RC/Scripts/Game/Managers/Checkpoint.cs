using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private PlayerSpawn playerSpawnScript;
    [SerializeField] Renderer model;
    Color colorOrig;
    private bool isActivated = false;

    private void Start()
    {
        colorOrig = model.material.color;

        playerSpawnScript = FindObjectOfType<PlayerSpawn>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")&& !isActivated)
        {
            if (playerSpawnScript != null)
            {
                playerSpawnScript.spawnPoint = transform;
                isActivated = true;
                StartCoroutine(FlashColor());
            }
        }
    }

    IEnumerator FlashColor()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = colorOrig;
    }
}
