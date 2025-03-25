
/*****************************************************************************
// File Name :         Checkpoint.cs
// Author :            Amber C. Cardamone
// Creation Date :     March 13th, 2025
//
// Brief Description : Checks if the player collides with it, and updates the checkpoint manager to use this object's position for respawn
*****************************************************************************/
using JetBrains.Annotations;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public CheckpointManager CM; //Grabs the game manager
    public AudioManager audioManager;
    public GameObject audioManagerObject;

    /// <summary>
    /// Grabs the GameManager script at the start.
    /// </summary>
    void Start()
    {
        CM = FindObjectOfType<CheckpointManager>();
        audioManagerObject = GameObject.Find("Audio Manager");
        if (audioManagerObject != null)
        {
            audioManager = audioManagerObject.GetComponent<AudioManager>();
        }
    }

    /// <summary>
    /// Checks if the player collides with this object, and if so, updates the GameManager.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // Checks if this object collides with an object with the "Player" tag
        if (other.gameObject.tag == "Player")
        {
            audioManager.checkpoint();
            // Sets the GameManager's checkpoint system to this current checkpoint
            CM.LastCheckPointPos = transform.position;
            // Removes the checkpoint, making it so the player can't accidentally go back to an older checkpoint
            Destroy(gameObject);
        }
    }
}
