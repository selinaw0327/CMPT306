using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public GameObject inventoryItem;
    private Inventory inventory;

    public Sprite itemSprite;


    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        itemSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
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
                        item.name = transform.name;
                        inventory.items[i] = transform.name;

                        item.GetComponent<Image>().sprite = itemSprite;
                        item.GetComponent<UseDrop>().sprite = itemSprite;
                        ItemsOnFloorList itemsOnFloorList = GameObject.FindGameObjectWithTag("ItemsOnFloor").GetComponent<ItemsOnFloorList>();
                        itemsOnFloorList.itemList.Remove(gameObject);
                        foreach (ItemData itemData in itemsOnFloorList.itemDataList)
                        {
                            if (itemData.position[0] == gameObject.transform.position.x && itemData.position[1] == gameObject.transform.position.y)
                            {
                                itemsOnFloorList.itemDataList.Remove(itemData);

                                break;
                            }
                        }
                        inventory.itemDataArr[i] = new InventoryItemData(item.GetComponent<UseDrop>());
                        break;
                    }
                }

            }

            inventory.quantity[inventory.IndexOf(name)] += 1;
            Destroy(gameObject);
            //Debug.Log(string.Join(", ", inventory.items));
            //Debug.Log(string.Join(", ", inventory.quantity));

        }
    }

}
