using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTrigger : MonoBehaviour
{
    List<IInterctiveObject> contenObjects = new List<IInterctiveObject>();
    [SerializeField] CarContentUI carContentUI;
    private void OnTriggerEnter(Collider other)
    {
        IInterctiveObject triggerableObject = other.GetComponent<IInterctiveObject>();
        if (triggerableObject != null)
        {
            if (!contenObjects.Contains(triggerableObject))
            {
                contenObjects.Add(triggerableObject);
            }
        }
        carContentUI.PrintList(contenObjects);
    }

    private void OnTriggerExit(Collider other)
    {
        IInterctiveObject triggerableObject = other.GetComponent<IInterctiveObject>();
        if (triggerableObject != null)
        {
            if (contenObjects.Contains(triggerableObject))
            {
                contenObjects.Remove(triggerableObject);
            }
        }
        carContentUI.PrintList(contenObjects);
    }
}
