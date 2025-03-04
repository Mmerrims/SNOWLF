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

    private void Quit(InputAction.CallbackContext context)
    {
        Application.Quit();
        print("Quit");
    }

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
}
