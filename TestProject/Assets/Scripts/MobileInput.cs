using UnityEngine;
using System;

public class MobileInput : MonoBehaviour, IInput
{
    [SerializeField] Joystick _moveJoystick;
    [SerializeField] Joystick _lookJoystick;

    public event Action OnJump;

    public void JumpButtonClicked()
    {
        OnJump.Invoke();
    }

    public Vector2 GetLookInput()
    {
        return _lookJoystick.GetInputDirection() / 10;
    }

    public Vector2 GetMovementInput()
    {
        return _moveJoystick.GetInputDirection();
    }
}
