using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseDrop : MonoBehaviour
{
    public GameObject item;
    private Inventory inventory;
    private GameObject slot;
    public Sprite sprite;

    private int itemIndex;

    private Item.ItemType itemType;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        slot = transform.parent.gameObject;
        sprite = gameObject.GetComponent<Image>().sprite;

        itemIndex = inventory.IndexOf(name);

        itemType = item.GetComponent<Item>().itemType;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        switch (itemType)
        {
            case Item.ItemType.Fruit:
                player.GetComponent<PlayerStats>().TakeHunger(-10);
                break;
            default:
                break;        
        }

        UpdateQuantity();
    }

    public void Drop()
    {
        GetComponent<Spawn>().SpawnDroppedItem(name, sprite);
        UpdateQuantity();
    }

    private void UpdateQuantity()
    {
        inventory.quantity[itemIndex] -= 1;

        // if the quantity is 0
        if (inventory.quantity[itemIndex] == 0)
        {
            Destroy(gameObject);
            inventory.items[itemIndex] = "";
            inventory.itemDataArr[inventory.IndexOf(slot)] = null;
            inventory.occupied[inventory.IndexOf(slot)] = false;
            inventory.inventoryItems[inventory.IndexOf(slot)] = null;
        }
    }
}
