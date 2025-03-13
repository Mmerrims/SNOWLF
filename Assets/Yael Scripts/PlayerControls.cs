/*******************************************************************
// File Name :         PlayerControlls.cs
// Author :            Yael Martoral
// Creation Date :     3/11/2025
//
// Brief Description : It controls actions that player can take outside
// of the main control scheme of controlling the snolf ball
/********************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public PlayerInput MPI;
    private InputAction restart;
    private InputAction quit;
    [SerializeField] private bool gameRestarting = false;
    [SerializeField] private CheckpointManager _checkpointManager;

    //It states Action Map actions like restart and quit so that they can later be called in game
    private void Awake()
    {
        _checkpointManager = FindObjectOfType<CheckpointManager>();
        print(_checkpointManager.LastCheckPointPos);
        transform.position = _checkpointManager.LastCheckPointPos;

        restart = MPI.currentActionMap.FindAction("Restart");
        quit = MPI.currentActionMap.FindAction("Quit");

        restart.started += Restart;
        quit.started += Quit;
    }

    //It closes and quits the game
    private void Quit(InputAction.CallbackContext context)
    {
        Application.Quit();
        print("Quit");
    }

    //When restart is called when the player only when the player presses the requiered button press 
    private void Restart(InputAction.CallbackContext context)
    {
        if (gameRestarting == false)
        {
            gameRestarting = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            return;
        }
    }

    //When the main snolf ball enters a trigger that has the 'Death' tag on it, it restart the current scene
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            if (gameRestarting == false)
            {
                gameRestarting = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                return;
            }
        }
    }
}
