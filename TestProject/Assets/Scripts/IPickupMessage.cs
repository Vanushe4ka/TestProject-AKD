using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface IPickupMessage
{
    public void ShowMessage(string message);
    public void HideMessage();
    public void SetPos(Vector3 pos);
    public void SetRot(Quaternion rot);
    event Action pickUp;
    event Action put;

}
