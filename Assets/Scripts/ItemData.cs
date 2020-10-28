using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

 [System.Serializable]
public class ItemData 
{
    
    

    public string name;
    
    
    public float[] position;

    public ItemData(Item item)
    {
  
        position = new float[2];
        position[0] = item.transform.position.x;
        position[1] = item.transform.position.y;

        name = item.name;
    }

}
