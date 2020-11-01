using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 [System.Serializable]
public class InventoryData 
{
    public InventoryItemData[] itemDataArr;
    public bool[] isFull;
    
    public InventoryData(Inventory inventory){
        itemDataArr = inventory.itemDataArr;
        isFull = inventory.isFull;
    }


}
