using UnityEngine;

public class ParManager : MonoBehaviour
{
    private static ParManager instance;
    private float playerPar;
    private ParSetter _parSetter;

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
        _parSetter = FindObjectOfType<ParSetter>();
    }

    public void AddHit()
    {
        playerPar += 1;

        if (_parSetter != null)
        {
            if (_parSetter.LevelPar > playerPar)
            {
                print("Under Par");
            }
            else if (_parSetter.LevelPar == playerPar)
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
