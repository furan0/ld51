using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private bool _isUsingGamepad = false;
    public bool isUsingGamepad {
        get { return _isUsingGamepad; }
    }

    private MainManager manager;
    public bool enableInput = true;

    // Start is called before the first frame update
    void Awake()
    {
        manager = GetComponent<MainManager>();
        Assert.IsNotNull(manager);
    }

    public void onMove(InputAction.CallbackContext context_) 
    {
        if(!enableInput)
            return;

        Vector2 move = context_.ReadValue<Vector2>();
        manager.Player.GetComponent<CanMove>()?.moveToward(new Vector3(move.x, move.y));
    }

    public void onLook(InputAction.CallbackContext context_)
    {
        if(!enableInput)
            return;

        Vector2 value = context_.ReadValue<Vector2>();
        CanAim[] aimingComponents = manager.Player.GetComponentsInChildren<CanAim>();
        
        /*if(_isUsingGamepad) {
            //Gamepad -> Value == direction looked at
            //Ignore null vector  (stick released)
            if (value.Equals(Vector2.zero))
                return;
            
            Vector3 directionAimedTo = new Vector3(value.x, value.y);

            //Update aim components
            foreach (CanAim aimComp in aimingComponents)
            {
                aimComp.updateAimedDirection(directionAimedTo);
            }


        } else {
            //Mouse -> Value == position to look at after convertion to world space
            Vector3 positionAimedTo = Camera.main.ScreenToWorldPoint(new Vector3(value.x, value.y));
            positionAimedTo.z = 0;

            //Update aim components
            foreach (CanAim aimComp in aimingComponents)
            {
                aimComp.updateAimedPosition(positionAimedTo);
            }
        }*/

        //TODO

    }

    public void onShoot(InputAction.CallbackContext context_) 
    {
        if(!enableInput)
            return;
            
        if (context_.performed) {
            manager.Player.GetComponentInChildren<CanShoot>()?.shoot();
        }
    }

    public void OnControlSchemeChange(PlayerInput pi_) {
        checkControlScheme(pi_.currentControlScheme);
    }

    private void checkControlScheme(string controlSchemeName_) {
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
}
