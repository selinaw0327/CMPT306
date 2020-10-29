using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadRunner : MonoBehaviour
{
    public PlayerStats player;
    public ItemsOnFloorList itemsOnFloorList;

    public GameObject item;

    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        itemsOnFloorList = GameObject.FindGameObjectWithTag("ItemsOnFloor").GetComponent<ItemsOnFloorList>();

    }

    public void SaveAll() 
    {
        SavePlayer();
        SaveItemsOnFloor();
    }

    public void LoadAll()
    {
        LoadPlayer();
        LoadItemsOnFloor();
    }

    public void SavePlayer()
    {
        SaveLoad.SavePlayer(player);
    }
    public void LoadPlayer()
    {
        SaveLoad.LoadPlayer(player);
        
  
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
