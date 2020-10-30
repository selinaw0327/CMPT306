using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsOnFloorList : MonoBehaviour
{
    
    public List<ItemData> itemDataList = new List<ItemData>();
    public List<GameObject> itemList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        GameObject[] itemArray = GameObject.FindGameObjectsWithTag("Item");
        
        foreach(GameObject item in itemArray){
            
            itemDataList.Add(new ItemData(item.GetComponent<Item>()));
            itemList.Add(item);
            
        }
    }


}
