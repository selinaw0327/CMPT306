using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public bool[] occupied;
    public GameObject[] slots;
    public InventoryItemData[] itemDataArr;

    public GameObject inventory;

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

        for (int i = 0; i < slots.Length; i++)
        {

            slots[i].GetComponent<Slot>().UpdateQuantity(quantity[i]);

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
