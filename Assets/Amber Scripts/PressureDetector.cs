using UnityEngine;

public class PressureDetector : MonoBehaviour
{
    [SerializeField] private float _neededWeight;
    [SerializeField] private GameObject _wall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (other.transform.localScale.x >= _neededWeight)
            {
                print(other.transform.localScale);
                _wall.SetActive(false);
            }
        }
    }
}
