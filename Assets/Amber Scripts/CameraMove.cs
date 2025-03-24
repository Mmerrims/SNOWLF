/*****************************************************************************
// File Name :         Camera Move.cs
// Author :            Amber C. Cardamone
// Creation Date :     March 13th, 2025
//
// Brief Description : Accesses cinemachine to turn on/off camera movement, so you can aim the ball freely.
*****************************************************************************/
using Cinemachine;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook _cinemachineFreeLook;

    /// <summary>
    /// Checks the player's input, turns on/off the camera's speed
    /// </summary>
    public void Update()
    {
        // Checks if the mouse is not being held down
        if (Input.GetMouseButtonUp(1))
        {
            print("Down");
            // Turns off all speed on the cameras
            _cinemachineFreeLook.m_XAxis.m_MaxSpeed = 0f;
            _cinemachineFreeLook.m_YAxis.m_MaxSpeed = 0f;

        } 
        // Checks if the mouse is being held down
        else if (Input.GetMouseButtonDown(1))
        {
            print("Up");
            // Sets the speed back to the normal speed
            _cinemachineFreeLook.m_XAxis.m_MaxSpeed = 500f;
            _cinemachineFreeLook.m_YAxis.m_MaxSpeed = 4f;
        }
    }
}
