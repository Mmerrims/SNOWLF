using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParManager : MonoBehaviour
{
    private static ParManager instance;
    private float levelPar;
    private float playerPar;

    private void Awake()
    {
        // Checks if there is no instance of the GameManager in the scene
        if (instance == null)
        {
            // Makes the instance 
            instance = this;
            // Makes it so this object does not get destroyed on load
            DontDestroyOnLoad(instance);
        }
        else
        {
            // Makes it so there aren't multiple instances of the Game Manager
            Destroy(gameObject);
        }
    }

    public void AddHit()
    {
        playerPar += 1;
    }
}
