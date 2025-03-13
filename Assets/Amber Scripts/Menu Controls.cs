using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuObjects;
    [SerializeField] private GameObject CreditsObject;
    [SerializeField] private GameObject ControlsObject;
    [SerializeField] private CheckpointManager _checkpointManager;

    private void Start()
    {
        _checkpointManager = FindObjectOfType<CheckpointManager>();
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        _checkpointManager.NewLevel();
    }

    public void Quit()
    {
        Application.Quit();
        _checkpointManager.NewLevel();
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
        _checkpointManager.NewLevel();
    }

    public void Back()
    {
        MainMenuObjects.SetActive(true);
        CreditsObject.SetActive(false);
    }

    public void ShowCredits()
    {
        MainMenuObjects.SetActive(false);
        CreditsObject.SetActive(true);
    }

    public void HideCredits()
    {
        MainMenuObjects.SetActive(true);
        CreditsObject.SetActive(false);
    }

    public void ShowControls()
    {
        MainMenuObjects.SetActive(false);
        ControlsObject.SetActive(true);
    }

    public void HideControls()
    {
        MainMenuObjects.SetActive(true);
        ControlsObject.SetActive(false);
    }
}
