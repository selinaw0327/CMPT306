using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public bool[] occupied;
    public GameObject[] slots;
    public InventoryItemData[] itemDataArr;

    public GameObject inventory;

    public GameObject[] inventoryItems;

    private bool show = true;
    public string[] items;
    public int[] quantity;


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
            ChallengeMenu challengeMenu = GameObject.FindGameObjectWithTag("Challenges").GetComponent<ChallengeMenu>();
            challengeMenu.updateChallenge("inv");
            show = !show;
        }

        // Detecting alpha numbers for using inventory items
        if (Input.GetKeyDown(KeyCode.Alpha1)) Use(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) Use(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Use(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) Use(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) Use(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) Use(5);
        if (Input.GetKeyDown(KeyCode.Alpha7)) Use(6);
        if (Input.GetKeyDown(KeyCode.Alpha8)) Use(7);
        if (Input.GetKeyDown(KeyCode.Alpha9)) Use(8);
        if (Input.GetKeyDown(KeyCode.Alpha0)) Use(9);

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Slot>().UpdateQuantity(quantity[i]);
        }
    }

    public void Use(int i)
    {
        if (occupied[i])
        {
            inventoryItems[i].GetComponent<UseDrop>().Use();
        }
        else
        {
            Debug.Log("Slot " + i + " is empty.");
        }
    }

    public int IndexOf(GameObject slot)
    {
        return System.Array.IndexOf(slots, slot);
    }

    public int IndexOf(string itemName)
    {
        return System.Array.IndexOf(items, itemName);
    }
}
