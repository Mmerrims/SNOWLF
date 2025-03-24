/*****************************************************************************
// File Name :         Pressure Detector.cs
// Author :            Amber C. Cardamone
// Creation Date :     March 13th, 2025
//
// Brief Description : Checks the player's size, if they're large enough, they cause an object to disable.
*****************************************************************************/
using UnityEngine;

public class PressureDetector : MonoBehaviour
{
    [SerializeField] private float _neededWeight;
    [SerializeField] private GameObject _wall;
    [SerializeField] private Animator _anim;

    /// <summary>
    /// Checks the player's size, if they're large enough, they cause an object to disable
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // Checks if this collided with the player
        if (other.CompareTag("Player"))
        {
            // Checks if the player's scale is larger then the needed weight variable
            if (other.transform.localScale.x >= _neededWeight)
            {
                //print(other.transform.localScale);
                // Turns off an object
                _wall.SetActive(false);
                // Plays an animation of the pressure plate going down
                _anim.Play("PressurePlateOn");
            }
        }
    }
}
