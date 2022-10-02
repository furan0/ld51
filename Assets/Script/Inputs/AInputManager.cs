using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/** Base clas sfor all three input managers. Implements basic stuffs */
public abstract class AInputManager : MonoBehaviour
{
    public bool enableInput = true;

    [SerializeField] public enum E_InputScheme {
        UI,
        FPS,
        PSY
    }

    static private ControlScheme control;  // Singleton
    protected ControlScheme Control {
        get {
            if (control == null) {
                control = new ControlScheme();
            }
            return control;
        }
    }

    [SerializeField]
    protected bool _isUsingGamepad = false;
    public bool isUsingGamepad {
        get { return _isUsingGamepad; }
    }

    public void OnControlSchemeChange(PlayerInput pi_) {
        checkControlScheme(pi_.currentControlScheme);
    }

    protected void checkControlScheme(string controlSchemeName_) {
        if (controlSchemeName_.Equals("Gamepad")) {
            //Starting using gamepad
            Debug.Log("Using Gamepad");
            _isUsingGamepad = true;
        } else {
            //Starting using keyboard
            Debug.Log("Using Keyboard");
            _isUsingGamepad = false;
        }
    }

    protected void OnDisable() {
        enableInput = false;
    }

    protected void OnEnable() {
        enableInput = true;
    }

    public void switchScheme(E_InputScheme newScheme) {
        switch (newScheme) {
            case E_InputScheme.UI:
                Control.UI.Enable();
                Control.Psy.Disable();
                Control.FPS.Disable();
            break;

            case E_InputScheme.FPS:
                Control.UI.Disable();
                Control.Psy.Disable();
                Control.FPS.Enable();
            break;

            case E_InputScheme.PSY:
                Control.UI.Disable();
                Control.Psy.Enable();
                Control.FPS.Disable();
            break;
        }
    }
}
