using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip ballHitSFX;
    public AudioClip checkpointSFX;
    public AudioClip goalReachSFX;

    
    public void ballHit()
    {
        audioSource.PlayOneShot(ballHitSFX);
    }

    public void checkpoint()
    {
        audioSource.PlayOneShot(checkpointSFX);
    }

    public void goalReached()
    {
        audioSource.PlayOneShot(goalReachSFX);
    }

}
