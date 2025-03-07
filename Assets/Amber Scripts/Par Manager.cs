using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParManager : MonoBehaviour
{
    private static ParManager instance;
    private float levelPar;
    private float playerPar;
    [SerializeField] private LevelPar _levelPar;

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

    private void FixedUpdate()
    {
        _levelPar = FindObjectOfType<LevelPar>();

        if (_levelPar == null)
        {
            print("No level par found");
        }
    }

    public void AddHit()
    {
        playerPar += 1;
        
        if (_levelPar != null)
        {
            if (_levelPar.LevelsPar > playerPar)
            {
                print("Under Par");
            } 
            else if (_levelPar.LevelsPar == playerPar)
            {
                print("On Par");
            } 
            else
            {
                print("Above Par");
            }
        }
    }
}
