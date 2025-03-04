using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerLocation;

    private void Update()
    {
        this.transform.position = new Vector3(playerLocation.transform.position.x, playerLocation.transform.position.y + 12, playerLocation.transform.position.z - 12);
    }
}
