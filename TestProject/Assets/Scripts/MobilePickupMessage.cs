using System;
using UnityEngine;
using UnityEngine.UI;

public class MobilePickupMessage : MonoBehaviour, IPickupMessage
{
    public event Action pickUp;
    public event Action put;

    [SerializeField] private Button _button;
    [SerializeField] private Text _buttonText;

    private bool _isPickedUp = false;

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

        _buttonText.text = (_isPickedUp ? "Put " : "Pick up ") + message;
    }

    public void HideMessage()
    {
        _buttonText.text = string.Empty;
        gameObject.SetActive(false);
    }

    void ButtonPickUp()
    {
        _isPickedUp = true;
        pickUp.Invoke();
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(ButtonPut);
    }

    void ButtonPut()
    {
        _isPickedUp = true;
        put.Invoke();
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(ButtonPickUp);
    }
}
