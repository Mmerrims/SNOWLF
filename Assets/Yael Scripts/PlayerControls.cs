using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public PlayerInput MPI;
    private InputAction restart;
    private InputAction quit;

    private void Awake()
    {
        restart = MPI.currentActionMap.FindAction("Restart");
        quit = MPI.currentActionMap.FindAction("Quit");

        restart.started += Restart;
        quit.started += Quit;
    }

    private void Quit(InputAction.CallbackContext context)
    {
        Application.Quit();
        print("Quit");
    }

    private void Restart(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
