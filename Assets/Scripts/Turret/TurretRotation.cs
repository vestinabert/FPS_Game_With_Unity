using UnityEngine; 

 
public class TurretRotation : MonoBehaviour 
{ 
    [SerializeField] private float rotationSpeed; 
    [SerializeField] private Vector3 defaultRotation; 
    [SerializeField] private Transform turretHead; 
    [SerializeField] private StealthTargeting stealthTargeting; 

    private Transform player; 
    private Quaternion defaulRotationQuaternion; 
    private void Start() 
    { 
        player = Player.Instance.transform; 
        defaulRotationQuaternion = Quaternion.Euler(defaultRotation); 
    } 

    private Quaternion CalculateTargetRotation() 
    { 
        Vector3 _targetDirection = player.position - turretHead.position; 
        Quaternion _lookRotation = Quaternion.LookRotation(-_targetDirection); 
        _lookRotation.eulerAngles = new Vector3(0, _lookRotation.eulerAngles.y, 0); 
        return _lookRotation; 
    } 
    private void Update() 
    { 
        StealthState _currentState = stealthTargeting.CurrentState; 
        Quaternion _targetRotation = Quaternion.identity; 
        if (_currentState == StealthState.Acquired) 
        { 
            _targetRotation = CalculateTargetRotation(); 
        } 
        else 
        { 
            _targetRotation = defaulRotationQuaternion; 
        } 

        turretHead.rotation = Quaternion.RotateTowards(turretHead.rotation, _targetRotation, rotationSpeed * Time.deltaTime); 
    } 
} 