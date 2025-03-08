using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuObjects;
    [SerializeField] private GameObject CreditsObject;
    [SerializeField] private GameObject ControlsObject;
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
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
