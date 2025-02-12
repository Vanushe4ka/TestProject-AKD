using UnityEngine;
using System;
public interface IInput 
{
    Vector2 GetMovementInput(); 
    Vector2 GetLookInput();

    event Action OnJump;
}
