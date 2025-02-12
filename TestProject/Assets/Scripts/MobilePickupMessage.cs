using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MobilePickupMessage : MonoBehaviour, IPickupMessage
{
    public event Action pickUp;
    public event Action put;
    [SerializeField] Button _button;
    [SerializeField] Text _buttonText;
    bool isPickedUp = false;
    public void HideMessage()
    {
        _buttonText.text = "";
        gameObject.SetActive(false);
    }
    private void Start()
    {
        _button.onClick.AddListener(ButtonPickUp);
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
            _buttonText.text = "Put " + message;
        }
        else
        {
            _buttonText.text = "Pick up " + message;
        }
    }

    void ButtonPickUp()
    {
        isPickedUp = true;
        pickUp.Invoke();
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(ButtonPut);
    }
    void ButtonPut()
    {
        isPickedUp = true;
        put.Invoke();
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(ButtonPickUp);
    }
}
