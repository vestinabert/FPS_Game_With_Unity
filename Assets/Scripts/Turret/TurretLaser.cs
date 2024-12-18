using UnityEngine;

public class TurretLaser : MonoBehaviour
{
    private const float CHARACTER_RADIUS = 0.5f;
    private const float MAX_LASER_LENGTH = 100f;

    [SerializeField] private LineRenderer laserRenderer;
    [SerializeField] private StealthTargeting stealthTargeting;

    private Transform player;

    private void Start()
    {
        player = Player.Instance.transform;
        laserRenderer.positionCount = 2;
    }

    private void Update()
    {
        StealthState _currentState = stealthTargeting.CurrentState;
        if (_currentState == StealthState.Acquired)
        {
            laserRenderer.enabled = true;
            laserRenderer.SetPosition(0, transform.position);

            bool _hit = Physics.SphereCast(transform.position, CHARACTER_RADIUS, -transform.forward, out RaycastHit _hitInfo, MAX_LASER_LENGTH);
            if (_hit && _hitInfo.collider.gameObject == player.gameObject)
            {
                laserRenderer.SetPosition(1, _hitInfo.point);
            }
            else
            {
                laserRenderer.SetPosition(1, transform.position - transform.forward * MAX_LASER_LENGTH);
            }
        }
        else
        {
            laserRenderer.enabled = false;
        }
    }
}