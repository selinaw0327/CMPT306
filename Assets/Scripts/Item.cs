using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [System.Serializable]
    public enum ItemType
    {
        Fruit,
        Bread,
        Steak,
        CopperBar,
        CopperSword,
        SilverBar,
        SilverSword,
        IronBar,
        IronSword,
        GoldBar,
        GoldSword,
        ObsidianBar,
        ObsidianSword
    }

    public GameObject inventoryItem;
    private Inventory inventory;

    public Sprite itemSprite;
    
    public ItemType itemType;

    private bool added = false;


    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        itemSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        ChallengeMenu challengeMenu = GameObject.FindGameObjectWithTag("Challenges").GetComponent<ChallengeMenu>();
        if (collider.CompareTag("Player"))
        {
            if (!inventory.items.Contains(name))
            {
                for (int i = 0; i < inventory.slots.Length; i++)
                {
                    if (inventory.occupied[i] == false)
                    {
                        // ITEM CAN BE ADDED TO THE INVENTORY

                        inventory.occupied[i] = true;
                        GameObject item = Instantiate(inventoryItem, inventory.slots[i].transform, false);

                        item.GetComponent<AudioSource>().Play();

                        item.transform.SetSiblingIndex(0);
                        item.name = transform.name;
                        inventory.items[i] = transform.name;
                        inventory.inventoryItems[i] = item;
                        item.GetComponent<Image>().sprite = itemSprite;
                        item.GetComponent<UseDrop>().sprite = itemSprite;
                        item.GetComponent<UseDrop>().itemType = itemType;
                        ItemsOnFloorList itemsOnFloorList = GameObject.FindGameObjectWithTag("ItemsOnFloor").GetComponent<ItemsOnFloorList>();
                        itemsOnFloorList.itemList.Remove(gameObject);
                        inventory.itemDataArr[i] = new InventoryItemData(item.GetComponent<UseDrop>());
                        break;
                    }
                }                
            }

            if (!added)
            {
                inventory.inventoryItems[inventory.IndexOf(name)].GetComponent<AudioSource>().Play();

                inventory.quantity[inventory.IndexOf(name)] += 1;
                added = true;
                if(name  == "Copper Bar") {
                    challengeMenu.updateChallenge("10cop");
                }
                challengeMenu.updateChallenge("pickup");
                Destroy(gameObject);
            }

        }
    }

}
