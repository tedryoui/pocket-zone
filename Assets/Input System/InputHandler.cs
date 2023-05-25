using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private InputActionsMap _actions;

    private void Awake()
    {
        if (_instance == null || _instance != this)
        {
            _instance = this;
            
            Initialize();
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
            
            _actions.Disable();
            _actions.Dispose();
        }
    }

    private void Initialize()
    {
        _actions = new InputActionsMap();
        _actions.Enable();

        _actions.Player.Movement.performed += (e) => OnJoystickMoved?.Invoke(e.ReadValue<Vector2>());
    }

    private static InputHandler _instance;

    public static Vector2 GetJoystickValue => _instance._actions.Player.Movement.ReadValue<Vector2>();
    public static Action<Vector2> OnJoystickMoved;

    public void Dispose() => _actions?.Dispose();
}
