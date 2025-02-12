using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IDisposable
{
    public Transform cameraTransform;
    public Rigidbody rigidbody;
    [SerializeField] private LayerMask scanLayer;
    [SerializeField] private float scanDistance = 10f;
    [SerializeField] Transform pickUpPos;
    IInterctiveObject pickedUpItem;
    [Inject] IPickupMessage _pickupMessage;
    public bool IsGrounded()
    {
        return true;
    }
    void Start()
    {
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
        if (pickedUpItem != null)
        {
            pickedUpItem.MoveTo(pickUpPos.position);
        }
        ScanEnvironment();
    }

    private void ScanEnvironment()
    {
        Vector3 rayOrigin = cameraTransform.position;
        Vector3 rayDirection = cameraTransform.forward;
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, scanDistance, scanLayer))
        {
            IInterctiveObject obj = hit.collider.GetComponent<IInterctiveObject>();
            if (obj != null)
            {
                _pickupMessage.ShowMessage(obj.GetName());
                _pickupMessage.SetPos(Vector3.Lerp(cameraTransform.position, hit.point,0.75f));
                _pickupMessage.SetRot(cameraTransform.rotation);
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
        if (pickedUpItem == null)
        {
            Vector3 rayOrigin = cameraTransform.position;
            Vector3 rayDirection = cameraTransform.forward;
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, scanDistance, scanLayer))
            {
                IInterctiveObject obj = hit.collider.GetComponent<IInterctiveObject>();
                if (obj != null)
                {
                    pickedUpItem = obj;
                    pickedUpItem.PickUp();
                    _pickupMessage.ShowMessage(obj.GetName());
                }
            }
        }
    }

    private void OnPut()
    {
        if (pickedUpItem != null)
        {
            pickedUpItem.Put();
            pickedUpItem = null;
            _pickupMessage.HideMessage();
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (cameraTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(cameraTransform.position, cameraTransform.forward * scanDistance);
        }
    }

    
}
