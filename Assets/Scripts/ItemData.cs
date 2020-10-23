using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 [System.Serializable]
public class ItemData 
{
    public bool edible;
    public int Damage;

    public string name;
    
    Image icon;

    public ItemData(ItemController item)
    {
        edible = item.edible;
        Damage = item.Damage;
        name = item.name;
        icon = item.GetComponent<Image>();;

    }

}
