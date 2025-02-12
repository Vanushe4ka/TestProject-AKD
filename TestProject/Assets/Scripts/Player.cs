using Zenject;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IDisposable
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private LayerMask _scanLayer;
    [SerializeField] private float _scanDistance = 10f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundedHeigth = 2f;
    [SerializeField] private Transform _pickUpPos;
    [Inject] private IPickupMessage _pickupMessage;

    private IInterctiveObject _pickedUpItem;

    public Rigidbody Rigidbody { get; private set; }
    public Transform CameraTransform => _cameraTransform;

    public bool IsGrounded()
    {
        return (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _groundedHeigth, _groundLayer));
    }

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _pickupMessage.pickUp += OnPickUp;
        _pickupMessage.put += OnPut;
    }

    public void Dispose()
    {
        _pickupMessage.pickUp -= OnPickUp;
        _pickupMessage.put -= OnPut;
    }

    void Update()
    {
        if (_pickedUpItem != null)
        {
            _pickedUpItem.MoveTo(_pickUpPos.position);
        }
        ScanEnvironment();
    }

    private void ScanEnvironment()
    {
        Vector3 rayOrigin = _cameraTransform.position;
        Vector3 rayDirection = _cameraTransform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, _scanDistance, _scanLayer))
        {
            IInterctiveObject obj = hit.collider.GetComponent<IInterctiveObject>();
            if (obj != null)
            {
                _pickupMessage.ShowMessage(obj.Name);
                _pickupMessage.SetPos(Vector3.Lerp(_cameraTransform.position, hit.point,0.75f));
                _pickupMessage.SetRot(_cameraTransform.rotation);
            }
            else
            {
                _pickupMessage.HideMessage();
            }
        }
        else
        {
            _pickupMessage.HideMessage();
        }
    }

    private void OnPickUp()
    {
        if (_pickedUpItem == null)
        {
            Vector3 rayOrigin = _cameraTransform.position;
            Vector3 rayDirection = _cameraTransform.forward;
            
            if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, _scanDistance, _scanLayer))
            {
                IInterctiveObject obj = hit.collider.GetComponent<IInterctiveObject>();
                if (obj != null)
                {
                    _pickedUpItem = obj;
                    _pickedUpItem.PickUp();
                    _pickupMessage.ShowMessage(obj.Name);
                }
            }
        }
    }

    private void OnPut()
    {
        if (_pickedUpItem == null) return;

        _pickedUpItem.Put();
        _pickedUpItem = null;
        _pickupMessage.HideMessage();
    }

    private void OnDrawGizmosSelected()
    {
        if (_cameraTransform == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(_cameraTransform.position, _cameraTransform.forward * _scanDistance);
    }
}
