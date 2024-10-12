using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public GameObject fanTrap;
    private ParticleSystem windEffect;
    private AudioSource fanAudio;
    
    [SerializeField] float pushForce = 10f;
    [SerializeField] float activeTime = 2f;
    [SerializeField] float inactiveTime = 3f;
    

    private bool fanIsOn;
    private float timer = 0f;
    private Vector3 pushDir;

    private Animator fanAnimator;

    void Start()
    {
        pushDir = -fanTrap.transform.forward;
        windEffect = fanTrap.GetComponentInChildren<ParticleSystem>();
        fanAudio = fanTrap.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (fanIsOn)
        {
            if(timer>=activeTime)
            {
                fanIsOn = false;
                timer = 0f;
                windEffect.Stop();
                fanAudio.Stop();
            }
        }
        else
        {
            if(timer>=inactiveTime)
            {
                fanIsOn = true;
                timer = 0f;
                windEffect.Play();
                fanAudio.Play();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(fanIsOn && other.CompareTag("Player"))
        {
            Debug.Log("Fan is on and player detected.");

            Rigidbody playerRb = other.attachedRigidbody;
            
            if(playerRb != null)
            {
                Debug.Log("Applying force to player.");

                playerRb.AddForce(pushDir * pushForce, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("The player's rigidbody is not found.");
            }
        }
    }
}
