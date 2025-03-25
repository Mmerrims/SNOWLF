/*****************************************************************************
// File Name :         Level End Script.cs
// Author :            Amber C. Cardamone
// Creation Date :     March 11th, 2025
//
// Brief Description : Turns on the endscreen menu when you hit this object
*****************************************************************************/
using UnityEngine;

public class LevelEndScript : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    public AudioManager audioManager;
    public GameObject audioManagerObject;

    public void Start()
    {
        audioManagerObject = GameObject.Find("Audio Manager");
        if (audioManagerObject != null)
        {
            audioManager = audioManagerObject.GetComponent<AudioManager>();
        }
    }

    /// <summary>
    /// Checks if the player collided with this object, turns on the win screen
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // Checks if the player collides with this object
        if (other.CompareTag("Player"))
        {
            // Turns on the win screen object
            _winScreen.SetActive(true);
            audioManager.goalReached();

        }
    }
}
