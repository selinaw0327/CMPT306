using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemsOnFloorData 
{
    public List<ItemData> itemDataList = new List<ItemData>();
    public ItemsOnFloorData(ItemsOnFloorList itemsOnFloorList){
        itemDataList = itemsOnFloorList.itemDataList;
    }
}
