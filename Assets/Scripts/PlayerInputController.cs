using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private WormMovement _wormMovement;
    private WormWeaponHandler _wormWeaponHandler;
    public static PlayerInputController Instance { get; private set; }

    private float _aimValue = 0f;
    private float _moveValue = 0f;
    private float _rotateValue = 0f;



    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void FixedUpdate()
    {
        _wormWeaponHandler?.RotateWeapon(_aimValue);
        _wormMovement?.Move(_moveValue);
        _wormMovement?.Rotate(_rotateValue);
    }

    public void SetCurrentWorm(WormMovement wormMovement, WormWeaponHandler wormWeaponHandler)
    {
        _wormMovement = wormMovement;
        _wormWeaponHandler = wormWeaponHandler;
    }


    public void MoveFromInput(InputAction.CallbackContext context)
    {
        _moveValue = context.ReadValue<float>();
    }

    public void JumpFromInput(InputAction.CallbackContext context)
    {
        _wormMovement?.Jump();
    }

    public void AimFromInput(InputAction.CallbackContext context)
    {
        _aimValue = context.ReadValue<float>();
    }

    public void ShootFromInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _wormWeaponHandler.PreShoot();
        }
        if (context.canceled)
        {
            _wormWeaponHandler.Shoot();
        }

    }

    public void RotateFromInput(InputAction.CallbackContext context)
    {
        _rotateValue = context.ReadValue<float>();
    }

    public void SwapToGrenadeLauncher(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _wormWeaponHandler.SwapToGrenadeLauncher();
        }
    }

    public void SwapToShotGun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _wormWeaponHandler.SwapToShotGun();
        }
    }
}

