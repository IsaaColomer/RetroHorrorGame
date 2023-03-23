using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

enum Actions
{
    Error,
    TurnLeft,
    TurnRight,
    MoveForward,
    MoveBackward,
    LookUp,
    LookDown,
}
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Actions desiredAction;
    [SerializeField] private string inputAction;
    [SerializeField] private Camera pCamera;
    [SerializeField] private bool performingAction;
    [SerializeField] private float xRotationAmmount;
    [SerializeField] private float yRotationAmmount;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float pSpeed;
    [SerializeField] private Rigidbody rb;
    private void Awake()
    {
        performingAction = false;
        pCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!performingAction)
        {
            ResetActionsValues();
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    GetInputAction(KeyCode.W);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    GetInputAction(KeyCode.S);
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    GetInputAction(KeyCode.A);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    GetInputAction(KeyCode.D);
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    GetInputAction(KeyCode.R);
                }
                else if (Input.GetKeyDown(KeyCode.F))
                {
                    GetInputAction(KeyCode.F);
                }
            }
            
        }
        
    }
    public void GetInputAction(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.W:
               
                    performingAction = true;
                    inputAction = Actions.MoveForward.ToString(); ;
                    CheckInputAction();
                
                break;
            case KeyCode.S:
                
                    performingAction = true;
                    inputAction = Actions.MoveBackward.ToString(); ;
                    CheckInputAction();
                
                break;
            case KeyCode.D:
                
                    performingAction = true;
                    inputAction = Actions.TurnRight.ToString(); ;
                    CheckInputAction();
                
                break;
            case KeyCode.A:
                
                    performingAction = true;
                    inputAction = Actions.TurnLeft.ToString(); ;
                    CheckInputAction();
                
                break;
            case KeyCode.R:
                
                    performingAction = true;
                    inputAction = Actions.LookUp.ToString(); ;
                    CheckInputAction();
                
                break;
            case KeyCode.F:
                
                    performingAction = true;
                    inputAction = Actions.LookDown.ToString(); ;
                    CheckInputAction();
                
                break;
            default:
                break;
        }
    }
    void CheckInputAction()
    {
        switch (inputAction)
        {
            //- foward -
            case "MoveForward":
                desiredAction = Actions.MoveForward;
                    break;
            //- backward -
            case "MoveBackward":
                desiredAction = Actions.MoveBackward;
                break;
                    //- left -
            case "TurnLeft":
                desiredAction = Actions.TurnLeft;
                break;
            //- right -
            case "TurnRight":
                desiredAction = Actions.TurnRight;
                break;
            default:
                desiredAction= Actions.Error;
                break;
            //- Up -
            case "LookUp":
                desiredAction = Actions.LookUp;
                break;
            //- Down -
            case "LookDown":
                desiredAction = Actions.LookDown;
                break;
        }
        ApplyDesiredAction(desiredAction);

    }
    void ApplyDesiredAction(Actions action)
    {
        switch (action)
        {            
            case Actions.Error:
                Debug.Log("No such action exists");
                performingAction = false;
                break;
            case Actions.TurnLeft:
                pCamera.transform.rotation = Quaternion.Euler(pCamera.transform.rotation.eulerAngles.x, pCamera.transform.rotation.eulerAngles.y - xRotationAmmount, pCamera.transform.rotation.eulerAngles.z);
                performingAction = false;
                break;
            case Actions.TurnRight:
                pCamera.transform.rotation = Quaternion.Euler(pCamera.transform.rotation.eulerAngles.x, pCamera.transform.rotation.eulerAngles.y + xRotationAmmount, pCamera.transform.rotation.eulerAngles.z);
                performingAction = false;
                break;
            case Actions.MoveForward:
                rb.velocity = pCamera.transform.forward * pSpeed;
                performingAction = false;
                break;
            case Actions.MoveBackward:
                rb.velocity = -pCamera.transform.forward * pSpeed;
                performingAction = false;
                break;
            case Actions.LookDown:
                    pCamera.transform.rotation = Quaternion.Euler(Mathf.Clamp(pCamera.transform.rotation.eulerAngles.x, -60, 60) + yRotationAmmount, pCamera.transform.rotation.eulerAngles.y, pCamera.transform.rotation.eulerAngles.z);
                    performingAction = false;
                
                break;
            case Actions.LookUp:
                    pCamera.transform.rotation = Quaternion.Euler(Mathf.Clamp(pCamera.transform.rotation.eulerAngles.x, -60, 60) - yRotationAmmount, pCamera.transform.rotation.eulerAngles.y, pCamera.transform.rotation.eulerAngles.z);
                    performingAction = false;
                break;
            default:
                break;
        }
        desiredAction = Actions.Error;
    }
    void ResetActionsValues()
    {
        rb.velocity = Vector3.zero;
    }
}
