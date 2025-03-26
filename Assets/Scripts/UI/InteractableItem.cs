using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    private DialogData dialogData;
    private DialogDataManager dialogDataManager;

    private void Awake()
    {
        dialogData = GetComponent<DialogData>();
        dialogDataManager = GameObject.Find("DialogDataManager").GetComponent<DialogDataManager>();
    }

    private void Start()
    {
        dialogDataManager.AddDialogData(dialogData.id, dialogData.dialogs);
    }
}
