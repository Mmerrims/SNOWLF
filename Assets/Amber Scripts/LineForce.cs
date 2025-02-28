using UnityEngine;

public class LineForce : MonoBehaviour
{
    [SerializeField] private float _stopVelocity;
    [SerializeField] private float _shotPower;
    [SerializeField] private float _jumpPower;
    [SerializeField] private Vector3 _currentShotStrength;

    [SerializeField] private LineRenderer _lineRenderer;
    private Rigidbody rigidbody;

    private bool isIdle;
    private bool isAiming;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        isAiming = false;
        _lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (rigidbody.velocity.magnitude < _stopVelocity)
        {
            Stop();
        }

        //if(rigidbody.velocity.x > 10)
        //{
        //    rigidbody.velocity = new Vector3 (rigidbody.velocity.x / 2, rigidbody.velocity.y, rigidbody.velocity.z / 2);
        //}

        ProcessAim();

        //if (_currentShotStrength.x > 10000)
        //{
        //    rigidbody.velocity = Vector3.zero;
        //} 
        //else if (_currentShotStrength.x < -10000)
        //{
        //    rigidbody.velocity = Vector3.zero;
        //}

        //if (_currentShotStrength.z > 10000)
        //{
        //    rigidbody.velocity = Vector3.zero;
        //} 
        //else if (_currentShotStrength.z < -10000)
        //{
        //    rigidbody.velocity = Vector3.zero;
        //}
    }

    private void OnMouseDown()
    {
        if (isIdle)
        {
            isAiming = true;
        }
    }

    private void ProcessAim()
    {
        if (!isAiming || !isIdle)
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
        }
    }

    private void Shoot(Vector3 worldPoint)
    {
        isAiming = false;
        _lineRenderer.enabled = false;

        Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);

        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
        float strength = Vector3.Distance(transform.position, horizontalWorldPoint);

        _currentShotStrength = (direction * strength * _shotPower);
        _currentShotStrength = new Vector3 (direction.x * strength * _shotPower, _jumpPower ,direction.z * strength * _shotPower);
        
        rigidbody.AddForce(_currentShotStrength / 2);
        // This makes it so the ball cannot be shot while moving
       //isIdle = false;
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
        isIdle = true;
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
        if (Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, float.PositiveInfinity))
        {
            return hit.point;
        } else
        {
            return null;
        }
    }
}
