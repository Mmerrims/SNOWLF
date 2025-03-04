using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuObjects;
    [SerializeField] private GameObject CreditsObject;
    public void StartGame()
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
}
