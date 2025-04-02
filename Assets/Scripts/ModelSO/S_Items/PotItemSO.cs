using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PotItemSO : ItemSO, IItemUse, IDestroyableItem
{
    public string[] TargetItem;

    public void ItemUse()
    {
        
    }
}

public interface ITarget
{
    public void UseItemAction(string itemName);
}
