using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public GameObject quantity;

    private string itemName;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);

        }
    }

    public void UpdateQuantity(int i)
    {
        if (i == 0) quantity.GetComponent<Text>().text = "";
        else quantity.GetComponent<Text>().text = i.ToString();
    }

    public string Name()
    {
        return itemName;
    }
}
