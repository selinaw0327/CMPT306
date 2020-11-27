using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{

    public GameObject item;
    public GameObject spriteAtlas;

    private GameObject enemy;

    public bool tutorial;
    public bool bossRoom;

    private void Start()
    {
        enemy = gameObject;
    }

    public void Drop()
    {
        int chance = Random.Range(0, 100);

        switch (name)
        {
            case "Worm":
                if (chance < 84)
                {
                    NewItem("Iron Bar", spriteAtlas.GetComponent<SpriteAtlas>().ironBar, Item.ItemType.IronBar);
                }
                if (chance < 5)
                {
                    NewItem("Gold Bar", spriteAtlas.GetComponent<SpriteAtlas>().goldBar, Item.ItemType.GoldBar);
                }
                if (chance < 1)
                {
                    NewItem("Obsidian Bar", spriteAtlas.GetComponent<SpriteAtlas>().obsidianBar, Item.ItemType.ObsidianBar);
                }
                break;
            case "Rat":
                if (chance < 30)
                {
                    NewItem("Iron Bar", spriteAtlas.GetComponent<SpriteAtlas>().ironBar, Item.ItemType.IronBar);
                }
                if (chance < 20)
                {
                    NewItem("Gold Bar", spriteAtlas.GetComponent<SpriteAtlas>().goldBar, Item.ItemType.GoldBar);
                }
                if (chance < 10)
                {
                    NewItem("Obsidian Bar", spriteAtlas.GetComponent<SpriteAtlas>().obsidianBar, Item.ItemType.ObsidianBar);
                }
                break;
            case "Bat":
                if (chance < 10)
                {
                    NewItem("Iron Bar", spriteAtlas.GetComponent<SpriteAtlas>().ironBar, Item.ItemType.IronBar);
                }
                if (chance < 50)
                {
                    NewItem("Gold Bar", spriteAtlas.GetComponent<SpriteAtlas>().goldBar, Item.ItemType.GoldBar);
                }
                if (chance < 20)
                {
                    NewItem("Obsidian Bar", spriteAtlas.GetComponent<SpriteAtlas>().obsidianBar, Item.ItemType.ObsidianBar);
                }
                break;
            default:
                break;
        }
    }

    public void BossRoomDrop() {
        switch(name) {
            case "Worm":
                NewItem("Apple", spriteAtlas.GetComponent<SpriteAtlas>().apple, Item.ItemType.Fruit);
                break;
            case "Rat":
                NewItem("Bread", spriteAtlas.GetComponent<SpriteAtlas>().bread, Item.ItemType.Bread);
                break;
            case "Bat":
                NewItem("Steak", spriteAtlas.GetComponent<SpriteAtlas>().steak, Item.ItemType.Steak);
                break;
            default:
                break;
        }
    }

    public void BossRewardDrop() {
        switch(name) {
            case "zombie":
                NewItem("CopperArmour", spriteAtlas.GetComponent<SpriteAtlas>().copperArmour, Item.ItemType.CopperArmour);
                break;
            case "Skeleton":
                NewItem("ObsidianArmour", spriteAtlas.GetComponent<SpriteAtlas>().copperArmour, Item.ItemType.ObsidianArmour);
                break;
            default:
                break;
        }
    }

    private void NewItem(string itemName, Sprite itemSprite, Item.ItemType itemType)
        {
            // Random new item spawn spot
            float x = enemy.transform.position.x - (1 / Random.Range(1, 11));
            float y = enemy.transform.position.y - (1 / Random.Range(1, 11));

            Vector2 spot = new Vector2(x, y);

            GameObject newItem = Instantiate(item, spot, Quaternion.identity, GameObject.Find("Environment").transform);
            newItem.name = itemName;

            newItem.GetComponent<Item>().itemType = itemType;

            newItem.GetComponent<SpriteRenderer>().sprite = itemSprite;
            newItem.GetComponent<Item>().itemSprite = itemSprite;

            ItemsOnFloorList itemsOnFloorList = GameObject.FindGameObjectWithTag("ItemsOnFloor").GetComponent<ItemsOnFloorList>();
            itemsOnFloorList.itemDataList.Add(new ItemData(newItem.GetComponent<Item>()));
            itemsOnFloorList.itemList.Add(newItem);
        }
}