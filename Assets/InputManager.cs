using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    [SerializeField] private InputActionReference A;
    [SerializeField] private InputActionReference B;
    [SerializeField] private InputActionReference Move;

    [SerializeField] private Action OnAGameplay;
    [SerializeField] private Action OnBGameplay;
    [SerializeField] private Action OnAReleaseGameplay;
    [SerializeField] private Action OnBReleaseGameplay;
    [SerializeField] private Action<float, bool> MoveGameplay;

    private bool AHeld;
    private bool BHeld;

    private void Awake()
    {
        EnableGameplayControls(true);

        A.action.performed += DoA;
        B.action.performed += DoB;
        A.action.canceled += DoA;
        B.action.canceled += DoB;
        Move.action.performed += DoMove;
        Move.action.canceled += DoMove;
    }

    private void DoA(CallbackContext ctx)
    {
        //if (BHeld) return;
        if (ctx.performed)
        {
            AHeld = true;
            controller.Lock(true);
        }
        if (AHeld && ctx.canceled)
        {
            AHeld = false;
            controller.Release(true);
        }
    }

    private void DoB(CallbackContext ctx)
    {
        //if (AHeld) return;
        if (ctx.performed)
        {
            BHeld = true;
            controller.Lock(false);
        }
        if (BHeld && ctx.canceled)
        {
            BHeld = false;
            controller.Release(false);
        }
    }

    private void DoMove(CallbackContext ctx)
    {
        controller.TriggerOffset(ctx.ReadValue<float>(), ctx.performed);
    }

    private void OnDestroy()
    {
        EnableGameplayControls(false);

        A.action.performed -= DoA;
        B.action.performed -= DoB;
        A.action.canceled -= DoA;
        B.action.canceled -= DoB;
        Move.action.performed -= DoMove;
        Move.action.canceled -= DoMove;
    }

    private void EnableGameplayControls(bool controls)
    {
        if (controls)
        {
            A.action.Enable();
            B.action.Enable();
            Move.action.Enable();
        } else
        {
            A.action.Disable();
            B.action.Disable();
            Move.action.Disable();
        }
    }
}
