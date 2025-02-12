using UnityEngine;
using Zenject;
using System;

public class DesctopInput : IInput, ITickable
{
    public Vector2 GetMovementInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public Vector2 GetLookInput()
    {
        return new Vector2( Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    public bool GetJumpInput()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public event Action OnJump;

    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump?.Invoke(); 
        }
    }
}
