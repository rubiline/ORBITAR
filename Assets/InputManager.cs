using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionReference A;
    [SerializeField] private InputActionReference B;

    [SerializeField] private UnityEvent OnAGameplay;
    [SerializeField] private UnityEvent OnBGameplay;
    [SerializeField] private UnityEvent OnAReleaseGameplay;
    [SerializeField] private UnityEvent OnBReleaseGameplay;

    private bool AHeld;
    private bool BHeld;

    private void Awake()
    {
        A.action.Enable();
        B.action.Enable();

        A.action.performed += DoA;
        B.action.performed += DoB;        
        A.action.canceled += DoA;
        B.action.canceled += DoB;

    }

    private void DoA(CallbackContext ctx)
    {
        if (BHeld) return;
        if (ctx.performed)
        {
            AHeld = true;
            OnAGameplay?.Invoke();
        }
        if (AHeld && ctx.canceled)
        {
            AHeld = false;
            OnAReleaseGameplay?.Invoke();
        }
    }

    private void DoB(CallbackContext ctx)
    {
        if (AHeld) return;
        if (ctx.performed)
        {
            BHeld = true;
            OnBGameplay?.Invoke();
        }
        if (BHeld && ctx.canceled)
        {
            BHeld = false;
            OnBReleaseGameplay?.Invoke();
        }
    }
}
