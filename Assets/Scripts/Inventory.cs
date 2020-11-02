﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public bool[] isFull;
    public GameObject[] slots;
    public InventoryItemData[] itemDataArr;

    public GameObject inventory;

    private bool show = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            inventory.SetActive(!show);
            show = !show;
        }
    }

    public int IndexOf(GameObject slot)
    {
        return System.Array.IndexOf(slots, slot);
    }
}
