using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    [field: SerializeField]
    public Sprite Icon { get; private set; }
    [field: SerializeField]
    public string DisplayName { get; private set; }
    [field: SerializeField]
    [field: TextArea]
    public string Discription { get; private set; }
}

public interface IDestroyableItem
{

}

public interface IItemUse
{
    public void ItemUse();
}