using UnityEngine;

public class SimpleInteractiveObj : MonoBehaviour, IInterctiveObject
{
    [SerializeField] string _name;
    [SerializeField] private float moveSpeed = 5f;

    Rigidbody _rigidbody;

    public string Name => _name;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void PickUp()
    {
        _rigidbody.useGravity = false;
    }

    public void Put()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.useGravity = true;
    }

    public void MoveTo(Vector3 pos)
    {
        Vector3 direction = (pos - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, pos);

        _rigidbody.velocity = direction * moveSpeed * distance;
    }
}
