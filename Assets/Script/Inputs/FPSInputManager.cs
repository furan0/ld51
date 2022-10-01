using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class FPSInputManager : AInputManager, ControlScheme.IFPSActions
{
    private MainManager manager;

    // Start is called before the first frame update
    void Awake()
    {
        manager = GetComponent<MainManager>();
        Assert.IsNotNull(manager);
    }

    void Start() {
        Control.FPS.SetCallbacks(this);
        switchScheme(E_InputScheme.FPS); //DEBUG
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
        throw new System.NotImplementedException();
    }

    void ControlScheme.IFPSActions.OnMenu(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    void ControlScheme.IFPSActions.OnMove(InputAction.CallbackContext context)
    {
        if(!enableInput)
            return;

        Vector2 move = context.ReadValue<Vector2>();
        manager.Player.GetComponent<CanMove>()?.moveToward(new Vector3(move.x, 0, move.y));
    }

    void ControlScheme.IFPSActions.OnShoot(InputAction.CallbackContext context)
    {
        if(!enableInput)
            return;
            
        if (context.performed) {
            manager.Player.GetComponentInChildren<CanShoot>()?.shoot();
        }
    }
}
