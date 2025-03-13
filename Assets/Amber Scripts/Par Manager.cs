using TMPro;
using UnityEngine;

public class ParManager : MonoBehaviour
{
    private static ParManager instance;
    private float levelPar;
    private float playerPar;
    [SerializeField] private LevelPar _levelPar;
    [SerializeField] private GameObject _levelParText;
    [SerializeField] private GameObject _levelParScoreName;
    [SerializeField] private TMP_Text _levelParTextbox;
    [SerializeField] private TMP_Text _levelParScoreNameText;

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

        _levelParText = GameObject.Find("Player Score Text");
        _levelParScoreName = GameObject.Find("Score Name Text");
        if (_levelParText != null)
        {
            _levelParTextbox = _levelParText.GetComponent<TMP_Text>();

            _levelParTextbox.text = ("Score: " + playerPar);
        }

        if (_levelParScoreName != null)
        {
            _levelParScoreNameText = _levelParScoreName.GetComponent<TMP_Text>();
        }



        if (_levelPar == null)
        {
            print("No level par found");
        }

        if (_levelPar != null)
        {
            if (_levelPar.LevelsPar > playerPar && _levelParScoreName != null)
            {

                _levelParScoreNameText.text = ("Under Par!");
            }
            else if (_levelPar.LevelsPar == playerPar && _levelParScoreName != null)
            {
                _levelParScoreNameText.text = ("On Par!");
            }
            else
            {
                if (_levelParScoreName != null)
                {
                    _levelParScoreNameText.text = ("Above Par.");
                }
            }
        }
    }

    public void AddHit()
    {
        playerPar += 1;
        
        
    }
}
