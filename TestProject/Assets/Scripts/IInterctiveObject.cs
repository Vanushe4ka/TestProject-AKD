using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInterctiveObject 
{
    string GetName();
    public void MoveTo(Vector3 pos);
    public void PickUp();
    public void Put();

}
