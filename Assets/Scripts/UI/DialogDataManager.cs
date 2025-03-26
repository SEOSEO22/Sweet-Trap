using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogDataManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private GameObject scanObject;

    public void SetDialog(GameObject scanObj)
    {
        scanObject = scanObj;
        dialogText.text = scanObject.name;
    }
}
