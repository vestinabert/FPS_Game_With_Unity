using UnityEngine; 
using UnityEngine.UI; 

public class StealthTargeting : MonoBehaviour 
{ 

    [SerializeField] private Scanner scanner; 
    [SerializeField] private Image stealthFill; 
    [SerializeField] private Color defaultColor; 
    [SerializeField] private Color worriedColor; 
    [SerializeField] private Color angryColor; 

    [SerializeField] private Image stealthCircle; 
    [SerializeField] private float fillSpeed = 0.5f; 


    private Transform player; 

    public StealthState CurrentState { get; private set; } = StealthState.None; 

    private void Awake() 

    { 
        stealthFill.fillAmount = 0; 
        stealthFill.color = Color.white; 
        stealthCircle.enabled = false; 
    }

    private void Start() 

    { 
        player = Player.Instance.transform; 
    } 


    private void UpdateStateBasedOnTarget() 
    { 
        Transform detectedTarget = scanner.DetectedTarget; 

        if (detectedTarget != null) 
        { 
            if (CurrentState == StealthState.None) 
            { 
                Debug.Log("isijunge");
                ChangeState(StealthState.Reacted); 
            } 
            else if (CurrentState == StealthState.Reacted && stealthFill.fillAmount >= 1) 
            { 
                ChangeState(StealthState.Acquired); 
            } 

            if (CurrentState == StealthState.Reacted) 
            { 
                stealthFill.fillAmount += fillSpeed * Time.deltaTime; 
            } 

            stealthCircle.enabled = true; 

        } 

        else 
        { 
            if (CurrentState == StealthState.Reacted) 
            { 
                stealthFill.fillAmount -= fillSpeed * Time.deltaTime; 

                if (stealthFill.fillAmount <= 0) 

                { 
                    stealthFill.fillAmount = 0; 
                    ChangeState(StealthState.None); 
                } 

            } 

            else if (CurrentState == StealthState.None) 
            { 
                stealthCircle.enabled = false; 
            } 
        } 
    } 

    private void UpdateCircleRotation() 
    { 
        Vector3 playerForward = player.forward; 
        Vector3 directionToCamera = transform.position - player.position; 

        playerForward.y = 0; 
        directionToCamera.y = 0; 
        float signedAngle = Vector3.SignedAngle(playerForward, directionToCamera, Vector3.down); 

        stealthCircle.transform.rotation = Quaternion.Euler(signedAngle * Vector3.forward); 

    } 

    private void ChangeState(StealthState newState) 
    { 
        CurrentState = newState; 

        switch (newState) 
        { 
            case StealthState.Reacted: 
            { 
                stealthFill.color = worriedColor; 
                break; 
            } 
            case StealthState.Acquired: 
            { 
                stealthFill.color = angryColor; 
                stealthFill.fillAmount = 1; 

                break; 
            } 
            case StealthState.None: 
            { 
                stealthFill.color = defaultColor; 

                stealthFill.fillAmount = 0; 

                break; 

            } 

        } 

    } 

    private void Update() 

    { 
        UpdateStateBasedOnTarget(); 
        UpdateCircleRotation(); 
    } 

} 