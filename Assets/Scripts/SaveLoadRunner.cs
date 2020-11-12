﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveLoadRunner : MonoBehaviour
{
    public PlayerStats player;
    public ItemsOnFloorList itemsOnFloorList;
    public Inventory inventory;

    public ChallengeMenu challenges;
    public GameObject item;
    public GameObject rock;
    public GameObject inventoryItem;
    public ProcGenDungeon map;
    public RockList rockList;
    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        itemsOnFloorList = GameObject.FindGameObjectWithTag("ItemsOnFloor").GetComponent<ItemsOnFloorList>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        challenges = GameObject.FindGameObjectWithTag("Challenges").GetComponent<ChallengeMenu>();
        map = GameObject.FindGameObjectWithTag("map").GetComponent<ProcGenDungeon>();
        rockList = GameObject.FindGameObjectWithTag("Enviroment").GetComponent<RockList>();
    }

    public void SaveAll() 
    {
        SavePlayer();
        SaveItemsOnFloor();
        SaveRocks();
        SaveInventory();
        SaveChallenges();
        SaveMapSeed();

    }

    public void LoadAll()
    {
        LoadMap();
        LoadPlayer();
        LoadRocks();
        LoadItemsOnFloor();
        LoadInventory();
        LoadChallenges();
        
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
    
    public void SaveMapSeed()
    {
        SaveLoad.SaveMapSeed(map);
    }
    public void LoadMap() 
    {
        foreach(GameObject createdObject in map.createdObjects){
            Destroy(createdObject);
        }
        for (int xMap = map.groundMap.cellBounds.xMin - 10; xMap <= map.groundMap.cellBounds.xMax + 10; xMap++)
        {
            for (int yMap = map.groundMap.cellBounds.yMin - 10; yMap <=  map.groundMap.cellBounds.yMax + 10; yMap++)
            {
                Vector3Int pos = new Vector3Int(xMap, yMap, 0);
            
                
                map.pitMap.SetTile(pos, null);
                map.groundMap.SetTile(pos, null);
                map.wallMap.SetTile(pos, null);
            }
        }

        SaveLoad.LoadMapSeed(map);
        map.createdObjects = new List<GameObject>();
        map.GenerateAll();
        foreach(GameObject createdObject in map.createdObjects){
            Destroy(createdObject);
        }
        
    }
    public void LoadInventory()
    {
        int i = 0;
        GameObject[] items = GameObject.FindGameObjectsWithTag("InventoryItem");
        foreach( GameObject item in items ){
            Destroy(item);
        }

        SaveLoad.LoadInventory(inventory);
        
        i=0;
        foreach(InventoryItemData itemData in inventory.itemDataArr){
            if(inventory.occupied[i]){
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

    public void SaveRocks()
    {
        SaveLoad.SaveRocks(rockList);
    }

    public void LoadRocks()
    {
        foreach(GameObject rock in  rockList.rockList){
            
            Destroy(rock);
            
        } 

        SaveLoad.LoadRocks(rockList);
        rockList.rockList = new List<GameObject>();

        foreach(RockData rockData in rockList.rockDataList){
            Vector2 position;
            position.x = rockData.position[0];
            position.y = rockData.position[1];

            GameObject newItem = Instantiate(rock, position, Quaternion.identity);
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


    public void SaveChallenges(){
        SaveLoad.SaveChallenges(challenges);

    }

    public void LoadChallenges(){
        SaveLoad.LoadChallenges(challenges);
    }


}
