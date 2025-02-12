using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
public class MovementHandler : MonoBehaviour, IDisposable
{
    [SerializeField] float speed = 5f;
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float verticalRotationLimit = 80f;
    [SerializeField] private float jumpForce;
    private float _verticalRotation;

    [SerializeField] Player player;
    IInput _input;
    [Inject]
    private void Init(IInput input)
    {
        _input = input;
        _input.OnJump += JumpHandle;
    }
    public void Dispose()
    {
        // Отписываемся от события прыжка
        _input.OnJump -= JumpHandle;
    }
    private void FixedUpdate()
    {
        MoveHandle();
        LookHandle();
    }
    void MoveHandle()
    {
        if (player == null || player.rigidbody == null)
        {
            Debug.LogError("Has not player or Rigidbody");
            return;
        }

        Vector2 moveInput = _input.GetMovementInput();
        Vector3 moveDirection = player.transform.forward * moveInput.y + player.transform.right * moveInput.x;
        player.rigidbody.MovePosition(moveDirection * speed + player.transform.position);
    }
    void LookHandle()
    {
        if (player == null || player.cameraTransform == null)
        {
            Debug.LogError("Has not player or camera");
            return;
        }

        Vector2 lookInput = _input.GetLookInput();

        // Поворот камеры по оси X (вертикальный поворот)
        _verticalRotation -= lookInput.y * sensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -verticalRotationLimit, verticalRotationLimit);
        player.cameraTransform.localEulerAngles = new Vector3(_verticalRotation, 0, 0);

        // Поворот игрока по оси Y (горизонтальный поворот)
        transform.Rotate(Vector3.up * lookInput.x * sensitivity);
    }
    void JumpHandle()
    {
        if (player == null || !player.IsGrounded())
        {
            return;
        }

        player.rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
