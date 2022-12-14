using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class FPSInputManager : AInputManager, ControlScheme.IFPSActions
{
    private MainManager manager;
    private Vector3 currentMoveTo;
    private Vector3 currentLook;

    [SerializeField] public float lookSensitivity = 1.0f;
    [SerializeField] public float minXAngle = -45.0f;
    [SerializeField] public float maxXAngle = 45.0f;
    private float xRotation = 0f;

    private GameSettings settings;

    public UnityEvent openMenu;

    // Start is called before the first frame update
    void Awake()
    {
        manager = GetComponent<MainManager>();
        Assert.IsNotNull(manager);
        currentMoveTo = Vector3.zero;
        currentLook = Vector3.forward;
        xRotation = 0;
    }

    void Start() {
        Control.FPS.SetCallbacks(this);
        DatabaseManager db = GameObject.FindGameObjectWithTag("Root")?.GetComponent<DatabaseManager>();
        settings = db?.settings;
    }

    new void OnEnable() {
        base.OnEnable();
        switchScheme(E_InputScheme.FPS);
        Cursor.lockState = CursorLockMode.Locked;
    }

    new void OnDisable() {
        base.OnDisable();
        Cursor.lockState = CursorLockMode.None;
        Control.FPS.Disable();
    }

    void Update() {
        handleLook();
        manager.Player.GetComponent<CanMove>()?.moveToward(Camera.main.transform.forward * currentMoveTo.y + Camera.main.transform.right * currentMoveTo.x);
    }

    void handleLook() {
        float sensibility = (settings != null)? settings.MouseVelocity : 1.0f;
        float lookX = currentLook.x * lookSensitivity * Time.deltaTime * sensibility;
        float lookY = currentLook.y * lookSensitivity * Time.deltaTime * sensibility;

        xRotation -= lookY;
        if (xRotation > 1000 || xRotation < -1000)
            xRotation = 0;
        else
            xRotation = Mathf.Clamp(xRotation, minXAngle, maxXAngle);
        
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        manager.Player.transform.Rotate(Vector3.up * lookX);
    }

    void ControlScheme.IFPSActions.OnMenu(InputAction.CallbackContext context)
    {
        openMenu?.Invoke();
        enabled = false;
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
            manager.Player.GetComponentInChildren<CanShot>()?.shoot(Camera.main.transform.rotation);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if(!enableInput)
            return;
        
        currentLook = context.ReadValue<Vector2>();
    }
}
