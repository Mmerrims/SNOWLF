/*****************************************************************************
// File Name :         Menu Controls.cs
// Author :            Amber C. Cardamone
// Creation Date :     March 13th, 2025
//
// Brief Description : Used as the code all buttons reference, opening/closing menus and changing scenes.
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuObjects;
    [SerializeField] private GameObject CreditsObject;
    [SerializeField] private GameObject ControlsObject;
    [SerializeField] private CheckpointManager _checkpointManager;

    /// <summary>
    /// Finds the object with a checkpoint manager in the scene
    /// </summary>
    private void Start()
    {
        _checkpointManager = FindObjectOfType<CheckpointManager>();
    }

    /// <summary>
    /// Loads in the next scene in the build order
    /// </summary>
    public void NextScene()
    {
        // Loads the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // Makes the checkpoint reload to 0,0
        _checkpointManager.NewLevel();
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void Quit()
    {
        // Quits the game
        Application.Quit();
        // Makes the checkpoint reload to 0,0
        _checkpointManager.NewLevel();
    }

    /// <summary>
    /// Loads the main menu scene
    /// </summary>
    public void Menu()
    {
        // Loads the main menu scene
        SceneManager.LoadScene("Main Menu");
        // Makes the checkpoint reload to 0,0
        _checkpointManager.NewLevel();
    }

    /// <summary>
    /// Turns on the main menu, turns off the credits object
    /// </summary>
    public void Back()
    {
        MainMenuObjects.SetActive(true);
        CreditsObject.SetActive(false);
    }

    /// <summary>
    /// Turns off the main menu, turns on the credits object
    /// </summary>
    public void ShowCredits()
    {
        MainMenuObjects.SetActive(false);
        CreditsObject.SetActive(true);
    }

    /// <summary>
    /// Turns on the main menu, turns off the credits object
    /// </summary>
    public void HideCredits()
    {
        MainMenuObjects.SetActive(true);
        CreditsObject.SetActive(false);
    }

    /// <summary>
    /// Turns off the main menu, turns on the controls object
    /// </summary>
    public void ShowControls()
    {
        MainMenuObjects.SetActive(false);
        ControlsObject.SetActive(true);
    }

    /// <summary>
    /// Turns on the main menu, turns off the controls object
    /// </summary>
    public void HideControls()
    {
        MainMenuObjects.SetActive(true);
        ControlsObject.SetActive(false);
    }
}
