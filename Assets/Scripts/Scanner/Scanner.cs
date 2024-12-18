using UnityEngine;

public class Scanner : MonoBehaviour
{
    private const float CHARACTER_RADIUS = 0.5f;

    [SerializeField] private float viewDistance;
    [SerializeField] private float viewAngle;

    private Transform player;
    private Transform detectedTarget;

    private void Start()
    {
        
        player = Player.Instance.transform;
    }

    public Transform DetectedTarget
    {
        get 
        { 
            return detectedTarget; 
        }
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }

        Vector3 _directionToTarget = player.position - transform.position;
        float _angle = Vector3.Angle(transform.forward, _directionToTarget);
        if (_angle <= viewAngle / 2)
        {
            // Using sphere cast, since standart raycasts don't work when using Character Controller.
            bool _hit = Physics.SphereCast(transform.position, CHARACTER_RADIUS, _directionToTarget, out RaycastHit _hitInfo, viewDistance);
            if (_hit && _hitInfo.transform == player)
            {
                detectedTarget = _hitInfo.transform;
            }
            else
            {
                detectedTarget = null;
            }
        }
        else
        {
            detectedTarget = null;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Gizmos.color = Color.blue;
        float yRotation = Mathf.Deg2Rad * transform.rotation.eulerAngles.y;
        float leftRad = Mathf.Deg2Rad * viewAngle / 2 + yRotation;
        float rightRad = -Mathf.Deg2Rad * viewAngle / 2 + yRotation;
        Vector3 leftAngle = new Vector3(Mathf.Sin(leftRad), 0, Mathf.Cos(leftRad)).normalized;
        Vector3 rightAngle = new Vector3(Mathf.Sin(rightRad), 0, Mathf.Cos(rightRad)).normalized;
        leftAngle *= viewDistance;
        rightAngle *= viewDistance;
        Gizmos.DrawLine(transform.position, transform.position + leftAngle);
        Gizmos.DrawLine(transform.position, transform.position + rightAngle);
    }
#endif
}