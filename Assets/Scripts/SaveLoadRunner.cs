using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveLoadRunner : MonoBehaviour
{
    public PlayerStats player;
    public ItemsOnFloorList itemsOnFloorList;
    public Inventory inventory;

    public GameObject item;
    public GameObject inventoryItem;
    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        itemsOnFloorList = GameObject.FindGameObjectWithTag("ItemsOnFloor").GetComponent<ItemsOnFloorList>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    public void SaveAll() 
    {
        SavePlayer();
        SaveItemsOnFloor();
        SaveInventory();

    }

    public void LoadAll()
    {
        LoadPlayer();
        LoadItemsOnFloor();
        LoadInventory();
    }

    public void SavePlayer()
    {
        SaveLoad.SavePlayer(player);
    }
    public void LoadPlayer()
    {
        SaveLoad.LoadPlayer(player);
    }
    public void SaveInventory()
    {
        SaveLoad.SaveInventory(inventory);
    }

    public void LoadInventory()
    {
        int i = 0;
        foreach(GameObject slot in inventory.slots){
            if(inventory.isFull[i]){
                
                foreach (Transform child in slot.transform)
                {
                    GameObject.Destroy(child.gameObject);

                }
            }
        }

        SaveLoad.LoadInventory(inventory);
        
        i=0;
        foreach(InventoryItemData itemData in inventory.itemDataArr){
            if(inventory.isFull[i]){
                GameObject newItem = Instantiate(inventoryItem, inventory.slots[i].transform, false);
            
                Texture2D spriteTexture = new Texture2D(itemData.spriteW, itemData.spriteH,TextureFormat.RGBA32, false );
                spriteTexture.LoadRawTextureData(itemData.spriteTex);
                spriteTexture.Apply();
                Sprite loadedSprite = Sprite.Create(spriteTexture, new Rect(0.0f,0.0f , itemData.spriteW, itemData.spriteH), Vector2.one);
                newItem.GetComponent<Image>().sprite = loadedSprite;
            }
            i++;
        }
    }
    public void SaveItemsOnFloor()
    {
        SaveLoad.SaveItemsOnFloor(itemsOnFloorList);
        
    }

    public void  LoadItemsOnFloor()
    {
        
        foreach(GameObject item in itemsOnFloorList.itemList){
            
            Destroy(item);
            
        }
        SaveLoad.LoadItemsOnFloor(itemsOnFloorList);
        itemsOnFloorList.itemList = new List<GameObject>();

        foreach(ItemData itemData in itemsOnFloorList.itemDataList){
            Vector2 position;
            position.x = itemData.position[0];
            position.y = itemData.position[1];

            GameObject newItem = Instantiate(item, position, Quaternion.identity);
            
            Texture2D spriteTexture = new Texture2D(itemData.spriteW, itemData.spriteH,TextureFormat.RGBA32, false );
            spriteTexture.LoadRawTextureData(itemData.spriteTex);
            spriteTexture.Apply();
            Sprite loadedSprite = Sprite.Create(spriteTexture, new Rect(0.0f,0.0f , itemData.spriteW, itemData.spriteH), Vector2.one);
            newItem.GetComponent<SpriteRenderer>().sprite = loadedSprite;
            
            item.GetComponent<Item>().itemSprite = loadedSprite;
            newItem.AddComponent<CircleCollider2D>();
            newItem.GetComponent<CircleCollider2D>().isTrigger = true;
            newItem.GetComponent<CircleCollider2D>().radius = 0.25f;
            GameObject.FindGameObjectWithTag("ItemsOnFloor").GetComponent<ItemsOnFloorList>().itemList.Add(newItem);
        }
    }

}
