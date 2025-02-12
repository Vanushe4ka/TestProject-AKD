using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CarContentUI : MonoBehaviour
{
    [SerializeField] TMP_Text contentText;
    [SerializeField] TMP_Text endText;
    [SerializeField] List<GameObject> tagetObjects = new List<GameObject>();
    List<IInterctiveObject> _targetContent = new List<IInterctiveObject>();
    private void Start()
    {
        endText.gameObject.SetActive(false);
        for (int i = 0; i< tagetObjects.Count; i++)
        {
            IInterctiveObject content = tagetObjects[i].GetComponent<IInterctiveObject>();
            if (content != null) { _targetContent.Add(content); }
        }
        PrintList(new List<IInterctiveObject>());
    }
    public void PrintList(List<IInterctiveObject> currnetContent)
    {
        bool isAll = true;
        contentText.text = "";
        for (int i = 0; i < _targetContent.Count; i++)
        {
            bool contain = currnetContent.Contains(_targetContent[i]);
            contentText.text += $"{(contain ? "<s>" : "")}-{_targetContent[i].GetName()}{(contain ? "</s>" : "")}\n";
            if (!contain) { isAll = false; }
        }
        if (isAll)
        {

            endText.gameObject.SetActive(true);
            endText.text = "The End!";
        }
    }
}
