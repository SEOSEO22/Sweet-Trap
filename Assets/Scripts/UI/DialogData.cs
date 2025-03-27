using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogData : MonoBehaviour
{
    public int id;
    public string[] dialogs;
    private int currentDialogIdx = 0;

    public int GetCurrentDialogIdx()
    {
        int returnValue = currentDialogIdx;

        if (currentDialogIdx < dialogs.Length) currentDialogIdx++;
        else
        {
            returnValue = -1;
            currentDialogIdx = 0;
        }

        return returnValue;
    }
}
