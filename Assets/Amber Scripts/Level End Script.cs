using UnityEngine;

public class LevelEndScript : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _winScreen.SetActive(true);
        }
    }
}
