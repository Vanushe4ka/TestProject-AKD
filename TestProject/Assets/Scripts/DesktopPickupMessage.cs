using System;
using UnityEngine;
using UnityEngine.UI;

public class DesktopPickupMessage : MonoBehaviour, IPickupMessage
{
    [SerializeField] private Text _text;

    public event Action pickUp;
    public event Action put;

    bool _isPickedUp = false;

    private void Start()
    {
        pickUp += OnPickUp;
        put += OnPut;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_isPickedUp)
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
        _text.text = string.Empty;
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
        _text.text = (_isPickedUp ? "Press E to put " : "Press E to pick up ") + message;
    }

    void OnPickUp()
    {
        _isPickedUp = true;
    }

    void OnPut()
    {
        _isPickedUp = false;
    }
}
