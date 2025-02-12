using UnityEngine;
using Zenject;
using System;

public class MovementHandler : MonoBehaviour, IDisposable
{
    [SerializeField] private Player _player;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _sensitivity = 2f;
    [SerializeField] private float _verticalRotationLimit = 80f;
    [SerializeField] private float _jumpForce;

    private float _verticalRotation;
    private IInput _input;

    [Inject]
    private void Init(IInput input)
    {
        _input = input;
        _input.OnJump += JumpHandle;
    }

    public void Dispose()
    {
        _input.OnJump -= JumpHandle;
    }

    private void FixedUpdate()
    {
        MoveHandle();
        LookHandle();
    }

    private void MoveHandle()
    {
        if (_player == null || _player.Rigidbody == null)
        {
            Debug.LogError("Has not player or Rigidbody");
            return;
        }

        Vector2 moveInput = _input.GetMovementInput();
        Vector3 moveDirection = _player.transform.forward * moveInput.y + _player.transform.right * moveInput.x;

        _player.Rigidbody.MovePosition(moveDirection * _speed + _player.transform.position);
    }

    private void LookHandle()
    {
        if (_player == null || _player.CameraTransform == null)
        {
            Debug.LogError("Has not player or camera");
            return;
        }

        Vector2 lookInput = _input.GetLookInput();

        _verticalRotation -= lookInput.y * _sensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -_verticalRotationLimit, _verticalRotationLimit);
        _player.CameraTransform.localEulerAngles = new Vector3(_verticalRotation, 0, 0);

        transform.Rotate(Vector3.up * lookInput.x * _sensitivity);
    }

    private void JumpHandle()
    {
        if (_player == null || !_player.IsGrounded()) return;

        _player.Rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}
