using UnityEngine;

public interface IInterctiveObject 
{
    public string Name { get; }
    public void MoveTo(Vector3 pos);
    public void PickUp();
    public void Put();
}
