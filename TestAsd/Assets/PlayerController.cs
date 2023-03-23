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
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform forwardStartPoint;
    [SerializeField] private Transform upStartForwardPoint;
    [SerializeField] private Transform upStartBackPoint;
    public float forwardRaycastDistance;
    [SerializeField] private Vector3 desiredMovePoint;
    private void Awake()
    {
        performingAction = false;
        pCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();
        forwardStartPoint = GameObject.Find("RaycastForwardStartPoint").GetComponent<Transform>();
        upStartForwardPoint = GameObject.Find("RaycastUpFrontStartPoint").GetComponent<Transform>();
        upStartBackPoint = GameObject.Find("RaycastUpBackStartPoint").GetComponent<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastForDebug();
        if (!performingAction)
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
                else if (Input.GetKeyDown(KeyCode.F))
                {
                    GetInputAction(KeyCode.F);
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    GetInputAction(KeyCode.R);
                }
            }
            
        }
        
    }
    void RaycastForDebug()
    {
        RaycastHit hit;
        RaycastHit hit2;
        if (!Physics.Raycast(forwardStartPoint.position, forwardStartPoint.forward, out hit, forwardRaycastDistance))
        {
            if (Physics.Raycast(upStartForwardPoint.position, -upStartForwardPoint.up, out hit2, forwardRaycastDistance))
            {
                desiredMovePoint = hit2.point;
                Debug.DrawLine(upStartForwardPoint.position, hit2.point, Color.green);
            }
            else
            {
                desiredMovePoint = Vector3.zero;
            }
            
        }
        else
        {
            Debug.DrawLine(upStartForwardPoint.position, hit.point, Color.cyan);
            if (Physics.Raycast(hit.point, -upStartBackPoint.transform.up, out hit2, forwardRaycastDistance))
            {
                desiredMovePoint = hit2.point;
                Debug.DrawLine(hit.point, hit2.point, Color.yellow);
            }
            else
            {
                desiredMovePoint = Vector3.zero;
            }
        }
    }
    bool CanGoForward()
    {
        bool ret = false;
        RaycastHit hit;
        RaycastHit hit2;
        if(!Physics.Raycast(forwardStartPoint.position, forwardStartPoint.forward, out hit, forwardRaycastDistance))
        {
            if(Physics.Raycast(upStartForwardPoint.position, -upStartForwardPoint.up, out hit2, forwardRaycastDistance))
            {
                desiredMovePoint = hit2.point;
                ret = true;
            }
            else
            {
                desiredMovePoint = Vector3.zero;
                ret = false;
            }
        }
        else
        {
            if (Physics.Raycast(hit.point, -upStartBackPoint.transform.up, out hit2, forwardRaycastDistance))
            {
                desiredMovePoint = hit2.point;
                ret = true;
            }
            else
            {
                desiredMovePoint = Vector3.zero;
                ret = false;
            }
        }
        return ret;
    }
    bool CanGoBackward()
    {
        bool ret = false;
        RaycastHit hit;
        RaycastHit hit2;
        if (!Physics.Raycast(forwardStartPoint.position, -forwardStartPoint.forward, out hit, forwardRaycastDistance))
        {
            if (Physics.Raycast(upStartBackPoint.position, -upStartBackPoint.up, out hit2, forwardRaycastDistance))
            {
                desiredMovePoint = hit2.point;
                ret = true;
            }
            else
            {
                desiredMovePoint = Vector3.zero;
                ret = false;
            }
        }
        else
        {
            if (Physics.Raycast(hit.point, -upStartBackPoint.transform.up, out hit2, forwardRaycastDistance))
            {
                desiredMovePoint = hit2.point;
                ret = true;
            }
            else
            {
                desiredMovePoint = Vector3.zero;
                ret = false;
            }
        }
        return ret;
    }
    public void GetInputAction(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.W:
               
                    performingAction = true;
                    inputAction = Actions.MoveForward.ToString();
                    CheckInputAction();
                
                break;
            case KeyCode.S:
                
                    performingAction = true;
                    inputAction = Actions.MoveBackward.ToString();
                    CheckInputAction();
                
                break;
            case KeyCode.D:
                
                    performingAction = true;
                    inputAction = Actions.TurnRight.ToString();
                    CheckInputAction();
                
                break;
            case KeyCode.A:
                
                    performingAction = true;
                    inputAction = Actions.TurnLeft.ToString();
                    CheckInputAction();
                
                break;
            case KeyCode.F:
                
                    performingAction = true;
                    inputAction = Actions.LookUp.ToString();
                    CheckInputAction();
                
                break;
            case KeyCode.R:
                
                    performingAction = true;
                    inputAction = Actions.LookDown.ToString();
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
                rb.transform.rotation = Quaternion.Euler(pCamera.transform.rotation.eulerAngles.x, pCamera.transform.rotation.eulerAngles.y - xRotationAmmount, pCamera.transform.rotation.eulerAngles.z);
                performingAction = false;
                break;
            case Actions.TurnRight:
                rb.transform.rotation = Quaternion.Euler(pCamera.transform.rotation.eulerAngles.x, pCamera.transform.rotation.eulerAngles.y + xRotationAmmount, pCamera.transform.rotation.eulerAngles.z);
                performingAction = false;
                break;
            case Actions.MoveForward:
                if(CanGoForward() && desiredMovePoint != Vector3.zero)
                {
                    rb.transform.position = new Vector3(desiredMovePoint.x, desiredMovePoint.y + 0.71f, desiredMovePoint.z);
                }
                performingAction = false;
                break;
            case Actions.MoveBackward:
                if (CanGoBackward() && desiredMovePoint != Vector3.zero)
                {
                    rb.transform.position = new Vector3(desiredMovePoint.x, desiredMovePoint.y + 0.71f, desiredMovePoint.z) ;
                }
                performingAction = false;
                break;
            case Actions.LookDown:
                if ((int)pCamera.transform.rotation.eulerAngles.x == 0 || (int)pCamera.transform.rotation.eulerAngles.x == 330)
                {
                    pCamera.transform.rotation = Quaternion.Euler(pCamera.transform.rotation.eulerAngles.x - yRotationAmmount, pCamera.transform.rotation.eulerAngles.y, pCamera.transform.rotation.eulerAngles.z);
                }
                performingAction = false;
                 
                break;
            case Actions.LookUp:
                if ((int)pCamera.transform.rotation.eulerAngles.x == 0 || (int)pCamera.transform.localRotation.eulerAngles.x == 30)
                {
                    pCamera.transform.rotation = Quaternion.Euler(pCamera.transform.rotation.eulerAngles.x + yRotationAmmount, pCamera.transform.rotation.eulerAngles.y, pCamera.transform.rotation.eulerAngles.z);
                }
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