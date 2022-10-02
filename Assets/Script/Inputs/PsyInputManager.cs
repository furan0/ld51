using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PsyInputManager : AInputManager, ControlScheme.IPsyActions
{
    private MainManager manager;

    public void OnHorizontal(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnMenu(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnVertical(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Awake()
    {
        manager = GetComponent<MainManager>();
        Assert.IsNotNull(manager);
    }

    void Start() {
        Control.Psy.SetCallbacks(this);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable() {
        switchScheme(E_InputScheme.PSY);
    }

    void Update() {
        
    }
}
