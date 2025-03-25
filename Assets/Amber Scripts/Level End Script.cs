using UnityEngine;

public class LevelEndScript : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    public AudioManager audioManager;
    public GameObject audioManagerObject;

    public void Start()
    {
        audioManagerObject = GameObject.Find("Audio Manager");
        if (audioManagerObject != null)
        {
            audioManager = audioManagerObject.GetComponent<AudioManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.goalReached();
            _winScreen.SetActive(true);
        }
    }
}
