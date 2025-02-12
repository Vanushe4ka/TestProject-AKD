using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInteractiveObj : MonoBehaviour,IInterctiveObject
{
    [SerializeField] string name;
    Rigidbody rigidbody;
    [SerializeField] private float moveSpeed = 5f;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    public string GetName()
    {
        return name;
    }

    public void PickUp()
    {
        rigidbody.useGravity = false;
    }

    public void Put()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.useGravity = true;
    }

    public void MoveTo(Vector3 pos)
    {
        Vector3 direction = (pos - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, pos);

        rigidbody.velocity = direction * moveSpeed * distance;
    }
}
