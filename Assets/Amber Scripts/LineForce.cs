/*****************************************************************************
// File Name :         Line Force.cs
// Author :            Amber C. Cardamone
// Creation Date :     March 11th, 2025
//
// Brief Description : Makes the ball able to be clicked on, drag, then release to launch the ball away from the mouse.
// Put on the player object.
*****************************************************************************/

using UnityEngine;

public class LineForce : MonoBehaviour
{
    [SerializeField] private float _stopVelocity;
    [SerializeField] private float _shotPower;
    [SerializeField] private float _jumpPower;
    [SerializeField] private Vector3 _currentShotStrength;
    [SerializeField] private LineRenderer _lineRenderer;
    private Rigidbody thisRigidbody;
    [SerializeField] private bool _isIdle;
    private bool isAiming;
    private bool initialTP;
    [SerializeField] private CheckpointManager _checkpointManager;
    [SerializeField] private ParManager _parManager;
    private LayerMask layerMask;

    /// <summary>
    /// Sets everything up.
    /// </summary>
    private void Awake()
    {
        //Finds the par manager script
        _parManager = FindObjectOfType<ParManager>();
        //Makes the ball use the Player Layermask
        layerMask = LayerMask.GetMask("Player");
        // Initial TP: makes the ball stay in position until the player clicks on the ball
        initialTP = true;
        // References the Checkpoint manager, then transforms the ball to be at the location of the last checkpoint
        _checkpointManager = FindObjectOfType<CheckpointManager>();
        //print(_checkpointManager.LastCheckPointPos);
        transform.position = _checkpointManager.LastCheckPointPos;
        // Grabs the rigidbody of the ball object
        thisRigidbody = GetComponent<Rigidbody>();
        // Sets the maximum angular velocity to a high amount so the ball can naturally roll
        thisRigidbody.maxAngularVelocity = 9999999999999;
        // Initially sets the ball to not aiming, so the player isn’t immediately
        isAiming = false;
        _isIdle = true;
        // Makes it so the line renderer for the ball drag isn't rendered
        _lineRenderer.enabled = false;
    }

    /// <summary>
    /// Constantly updates, checking if the ball should stop, shoot, or stay frozen until clicked.
    /// </summary>
    private void Update()
    {
        // Checks if the balls current velocity is lower than the stop velocity
        if (thisRigidbody.velocity.magnitude < _stopVelocity)
        {
            // Activates the Stop void, making the ball stop
            Stop();
        }
        // Constantly checks the state of the mouse and if the ball should be launched
        ProcessAim();
        // Checks if the ball should still be locked in place on the initial spawn
        if (initialTP)
        {
            // Constantly teleports the ball to the last checkpoint, holding it in place until the player clicks on it
            transform.position = _checkpointManager.LastCheckPointPos;
        }
    }

    /// <summary>
    /// Checks if the player clicks down on the mouse
    /// </summary>
    private void OnMouseDown()
    {
        // Makes it so the ball stops being frozen midair
        initialTP = false;
        if (_isIdle)
        {
            isAiming = true;
        }
    }

    /// <summary>
    /// Checks if the ball will get shot, and casts the linerenderer to display between the mouse and the ball
    /// </summary>
    private void ProcessAim()
    {
        if (!isAiming || !_isIdle)
        {
            return;
        }
        // Finds the worldpoint/position of the mouse and the raycast its sending out
        Vector3? worldPoint = CastMouseClickRay();
        // Checks if the worldpoint has a value, if not, return as empty
        if (!worldPoint.HasValue)
        {
            return;
        }
        // Draws a linerenderer line between the two points
        DrawLine(worldPoint.Value);
        // Checks if the player releases the left mouse button
        if (Input.GetMouseButtonUp(0))
        {
            // Makes the shoot void launch the ball using the mouse's position
            Shoot(worldPoint.Value);
            //_isIdle = false;
        }
    }

    private void Shoot(Vector3 worldPoint)
    {
        if (_parManager != null)
        {
            _parManager.AddHit();
        }

        isAiming = false;
        _lineRenderer.enabled = false;

        Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);

        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
        float strength = Vector3.Distance(transform.position, horizontalWorldPoint);

        _currentShotStrength = (direction * strength * _shotPower);
        _currentShotStrength = new Vector3 (direction.x * strength * _shotPower, _jumpPower ,direction.z * strength * _shotPower);
        
        thisRigidbody.AddForce(_currentShotStrength / 2);
        // This makes it so the ball cannot be shot while moving
       //_isIdle = false;
    }

    private void DrawLine(Vector3 worldPoint)
    {
        Vector3[] positions =
        {
            transform.position,
            worldPoint
        };
        _lineRenderer.SetPositions(positions);
        _lineRenderer.enabled = true;
    }

    private void Stop()
    {
        thisRigidbody.velocity = Vector3.zero;
        thisRigidbody.angularVelocity = Vector3.zero;
        _isIdle = true;
    }

    private Vector3? CastMouseClickRay()
    {
        Vector3 screenMouusePosFar = new Vector3
            (
                Input.mousePosition.x,
                Input.mousePosition.y,
                Camera.main.farClipPlane
            );
        Vector3 screenMouusePosNear = new Vector3
            (
                Input.mousePosition.x,
                Input.mousePosition.y,
                Camera.main.nearClipPlane
            );
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMouusePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMouusePosNear);
        RaycastHit hit;
        if (Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, float.PositiveInfinity, layerMask))
        {
            return hit.point;
        } else
        {
            return null;
        }
    }
}
