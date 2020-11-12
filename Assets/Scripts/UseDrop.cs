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

    public Item.ItemType itemType;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        slot = transform.parent.gameObject;
        sprite = gameObject.GetComponent<Image>().sprite;

        itemIndex = inventory.IndexOf(name);

        //itemType = item.GetComponent<Item>().itemType;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log("Item Type: " + itemType.ToString());

        switch (itemType)
        {
            case Item.ItemType.Fruit:
                player.GetComponent<PlayerStats>().TakeHunger(-10);
                UpdateQuantity(-1);
                break;
            case Item.ItemType.SilverBar:
                if (inventory.quantity[itemIndex] < 10)
                {
                    Debug.Log("You need 10 silver bars to forge a silver sword");
                } else if (inventory.quantity[itemIndex] >= 10)
                {
                    UpdateQuantity(-10);
                    Sprite silverSword = GameObject.Find("Sprite Atlas").GetComponent<SpriteAtlas>().silverSword;
                    Forge("Silver Sword", Item.ItemType.SilverSword, silverSword);
                }
                break;
            default:
                break;        
        }
    }

    public void Forge(string name, Item.ItemType itemType, Sprite forgedSprite)
    {
        GetComponent<Spawn>().SpawnDroppedItem(name, itemType, forgedSprite);
    }

    public void Drop()
    {
        GetComponent<Spawn>().SpawnDroppedItem(name, itemType, sprite);
        UpdateQuantity(-1);
    }

    private void UpdateQuantity(int i)
    {
        inventory.quantity[itemIndex] += i;

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
