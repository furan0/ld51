using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class FPSInputManager : AInputManager, ControlScheme.IFPSActions
{
    private MainManager manager;
    private Vector3 currentMoveTo;

    [SerializeField] public float lookSensitivity = 1.0f;
     private float xRotation = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        manager = GetComponent<MainManager>();
        Assert.IsNotNull(manager);
        currentMoveTo = Vector3.zero;
    }

    void Start() {
        Control.FPS.SetCallbacks(this);
        switchScheme(E_InputScheme.FPS); //DEBUG
    }

    void FixedUpdate() {
        manager.Player.GetComponent<CanMove>()?.moveToward(Camera.main.transform.forward * currentMoveTo.y + Camera.main.transform.right * currentMoveTo.x);
    }

    void ControlScheme.IFPSActions.OnMenu(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    void ControlScheme.IFPSActions.OnMove(InputAction.CallbackContext context)
    {
        if(!enableInput)
            return;

        currentMoveTo = context.ReadValue<Vector2>();
    }

    void ControlScheme.IFPSActions.OnShoot(InputAction.CallbackContext context)
    {
        if(!enableInput)
            return;
            
        if (context.performed) {
            manager.Player.GetComponentInChildren<CanShot>()?.shoot();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if(!enableInput)
            return;

        Vector2 value = context.ReadValue<Vector2>();

        float lookX = value.x * lookSensitivity * Time.deltaTime;
        float lookY = value.y * lookSensitivity * Time.deltaTime;

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        manager.Player.transform.Rotate(Vector3.up * lookX);

    }
}
