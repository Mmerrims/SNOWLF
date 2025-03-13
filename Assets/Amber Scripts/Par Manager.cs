/*****************************************************************************
// File Name :         Par Manager.cs
// Author :            Amber C. Cardamone
// Creation Date :     March 13th, 2025
//
// Brief Description : Makes the level par function, showing the level's par at the end and if you succeeded or not.
*****************************************************************************/
using TMPro;
using UnityEngine;

public class ParManager : MonoBehaviour
{
    private static ParManager instance;
    private float playerPar;
    [SerializeField] private LevelPar _levelPar;
    [SerializeField] private GameObject _levelParText;
    [SerializeField] private GameObject _levelParScoreName;
    [SerializeField] private TMP_Text _levelParTextbox;
    [SerializeField] private TMP_Text _levelParScoreNameText;

    /// <summary>
    /// Makes it so this object/script can travel across scenes, keeping its information.
    /// </summary>
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

    /// <summary>
    /// Checks the current par of the scene, displays it to the player at the end of the level
    /// </summary>
    private void FixedUpdate()
    {
        // Finds the current object in the scene with the Level Par script
        _levelPar = FindObjectOfType<LevelPar>();
        // Finds the text object showing the player's score, with the object having the name "Player Score Text"
        _levelParText = GameObject.Find("Player Score Text");
        // Finds the text object showing the level's score, with the object having the name "Score Name Text"
        _levelParScoreName = GameObject.Find("Score Name Text");
        // Checks if the par text is found
        if (_levelParText != null)
        {
            // Finds the text component on the level par text object
            _levelParTextbox = _levelParText.GetComponent<TMP_Text>();
            // Changes the score displayed to the player's ending score
            _levelParTextbox.text = ("Score: " + playerPar);
        }

        // Checks if the score text was found
        if (_levelParScoreName != null)
        {
            // Makes the level's par text the same as the found level's par
            _levelParScoreNameText = _levelParScoreName.GetComponent<TMP_Text>();
        }

        // Checks if the level's par was found and the level's score name text was found
        if (_levelPar != null && _levelParScoreName != null)
        {
            // Checks if the level's par is greater then the player's par
            if (_levelPar.LevelsPar > playerPar)
            {
                // Makes the text display that the player got under par
                _levelParScoreNameText.text = ("Under Par!");
            }
            // Checks if the level's par is equal to the player's score
            else if (_levelPar.LevelsPar == playerPar)
            {
                // Makes the text display that the player got on par
                _levelParScoreNameText.text = ("On Par!");
            }
            // Automatically goes to this if the other two fail, meaning the player got above par
            else
            {
                // Makes the text display that the player got above par
                _levelParScoreNameText.text = ("Above Par.");
            }
        }
    }

    /// <summary>
    /// Adds a point to the player's par.
    /// </summary>
    public void AddHit()
    {
        playerPar += 1;
    }
}
