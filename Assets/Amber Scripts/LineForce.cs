using UnityEngine;

public class LineForce : MonoBehaviour
{
    [SerializeField] private float _stopVelocity;
    [SerializeField] private float _shotPower;
    [SerializeField] private float _jumpPower;
    [SerializeField] private Vector3 _currentShotStrength;

    [SerializeField] private LineRenderer _lineRenderer;
    private Rigidbody rigidbody;

    [SerializeField] private bool _isIdle;
    private bool isAiming;

    private bool initialTP;

    [SerializeField] private CheckpointManager _checkpointManager;
    [SerializeField] private ParManager _parManager;

    private LayerMask layerMask;

    private void Awake()
    {
        

        _parManager = FindObjectOfType<ParManager>();


        layerMask = LayerMask.GetMask("Player");

        initialTP = true;
        _checkpointManager = FindObjectOfType<CheckpointManager>();
        print(_checkpointManager.LastCheckPointPos);
        transform.position = _checkpointManager.LastCheckPointPos;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.maxAngularVelocity = 9999999999999;

        isAiming = false;
        _isIdle = true;
        _lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (rigidbody.velocity.magnitude < _stopVelocity)
        {
            Stop();
        }

        ProcessAim();
        
        if (initialTP)
        {
            transform.position = _checkpointManager.LastCheckPointPos;
        }
    }

    private void OnMouseDown()
    {
        initialTP = false;
        if (_isIdle)
        {
            isAiming = true;
        }
    }

    private void ProcessAim()
    {
        if (!isAiming || !_isIdle)
        {
            return;
        }

        Vector3? worldPoint = CastMouseClickRay();

        if (!worldPoint.HasValue)
        {
            return;
        }

        DrawLine(worldPoint.Value);

        if (Input.GetMouseButtonUp(0))
        {
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
        
        rigidbody.AddForce(_currentShotStrength / 2);
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
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
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
