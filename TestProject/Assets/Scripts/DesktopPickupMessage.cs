using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DesktopPickupMessage : MonoBehaviour, IPickupMessage
{
    [SerializeField] Text _text;

    public event Action pickUp;
    public event Action put;
    bool isPickedUp = false;
    private void Start()
    {
        pickUp += OnPickUp;
        put += OnPut;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPickedUp)
            {
                put.Invoke();
            }
            else
            {

                pickUp.Invoke();
            }
        }
    }
    public void HideMessage()
    {
        _text.text = "";
        gameObject.SetActive(false);
    }

    public void SetPos(Vector3 pos)
    {
        transform.position = pos;
    }

    public void SetRot(Quaternion rot)
    {
        transform.rotation = rot;
    }

    public void ShowMessage(string message)
    {
        gameObject.SetActive(true);
        if (isPickedUp)
        {
            _text.text = "Press E to put " + message;
        }
        else
        {
            _text.text = "Press E to pick up " + message;
        }
    }
    void OnPickUp()
    {
        isPickedUp = true;
    }
    void OnPut()
    {
        isPickedUp = false;
    }

    
}
