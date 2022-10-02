using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PsyInputManager : AInputManager, ControlScheme.IPsyActions
{
    private PsyManager manager;
    private float valueHorizontal = 0.0f;
    private float valueVertical = 0.0f;
    [SerializeField] float moveFactor = 3.0f;

    public void OnHorizontal(InputAction.CallbackContext context)
    {
        valueHorizontal = context.ReadValue<float>();
    }

    public void OnMenu(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnVertical(InputAction.CallbackContext context)
    {
        valueVertical = context.ReadValue<float>();
    }

    // Start is called before the first frame update
    void Awake()
    {
        manager = GetComponent<PsyManager>();
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
        //Yiiiiiin
        CanMoveClamped mover = manager.Yin.GetComponent<CanMoveClamped>();
        Vector3 nextPosition = mover.Position;
        nextPosition.x += valueHorizontal * Time.deltaTime * moveFactor;
        mover.Position = nextPosition;

        //Yaaaang
        mover = manager.Yang.GetComponent<CanMoveClamped>();
        nextPosition = mover.Position;
        nextPosition.y += valueVertical * Time.deltaTime * moveFactor;
        mover.Position = nextPosition;
    }
}
