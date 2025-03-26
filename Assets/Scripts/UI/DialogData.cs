using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogData : MonoBehaviour
{
    public int id;
    public int currentDialogIdx = 0;
    public string[] dialogs;
}
