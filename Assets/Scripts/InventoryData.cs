using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 [System.Serializable]
public class InventoryData 
{
    public InventoryItemData[] itemDataArr;
    public int[] quantity;
    public bool[] isFull;
    public string[] items;
    
    public InventoryData(Inventory inventory){
        quantity = inventory.quantity;
        itemDataArr = inventory.itemDataArr;
        isFull = inventory.occupied;
        items = inventory.items;
    }


}
