using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource FootstepSource;
    public AudioSource JumpSource;
    public AudioSource DashSource;
    public AudioClip DashClip;
    public AudioClip[] JumpClips;
    public float footstepDelayWalking = 0.5f;
    public float footstepDelayRunning = 0.3f;
    public float footstepDelayWallrunning = 0.4f;

    private Controller playerController;
    private bool isPlayingFootsteps;
    private bool hasPlayedDashSound = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<Controller>();
        isPlayingFootsteps = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleFootsteps();
    }

    public void PlayJumpSound()
    {
        PlayRandomJumpSound();
    }

    public void PlayDashSound()
    {
        if (!DashSource.isPlaying && !hasPlayedDashSound)
        {
            DashSource.PlayOneShot(DashClip);
            hasPlayedDashSound = true;
        }
    }

    public void ResetDashSound()
    {
        hasPlayedDashSound = false;
    }

    private void PlayRandomJumpSound()
    {
        if(JumpClips.Length > 0)
        {
            int randomIndex = Random.Range(0, JumpClips.Length);
            AudioClip randomJumpClip = JumpClips[randomIndex];
            JumpSource.PlayOneShot(randomJumpClip);
        }
    }

    private void HandleFootsteps()
    {
        if(Time.timeScale == 0)
        {
            FootstepSource.Pause();
            return;
        }
        
        
        if (playerController.isGrounded && playerController.IsMoving())
        {
            if (!isPlayingFootsteps)
            {
                float delay = playerController.state == Controller.MovementState.sprinting ? footstepDelayRunning : footstepDelayWalking;
                StartCoroutine(PlayFootsteps(delay));
            }
        }
        else if (playerController.isWallrunning)
        {
            if (!isPlayingFootsteps)
            {
                StartCoroutine(PlayFootsteps(footstepDelayWallrunning));
            }
        }
        else
        {
            FootstepSource.Pause();
        }
    }

    private IEnumerator PlayFootsteps(float delay)
    {
        isPlayingFootsteps = true;

        if(!FootstepSource.isPlaying)
        {
            FootstepSource.Play();
        }

        yield return new WaitForSecondsRealtime(delay);

        FootstepSource.Pause();

        isPlayingFootsteps = false;
    }
}
