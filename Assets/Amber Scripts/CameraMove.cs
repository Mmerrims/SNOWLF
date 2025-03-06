using Cinemachine;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook _cinemachineFreeLook;

    public void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            print("Down");
            _cinemachineFreeLook.m_XAxis.m_MaxSpeed = 0f;
            _cinemachineFreeLook.m_YAxis.m_MaxSpeed = 0f;

        } 
        else if (Input.GetMouseButtonDown(1))
        {
            print("Up");

            _cinemachineFreeLook.m_XAxis.m_MaxSpeed = 500f;
            _cinemachineFreeLook.m_YAxis.m_MaxSpeed = 4f;
        }
    }
}
