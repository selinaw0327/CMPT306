using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public GameObject bat;
    public GameObject worm;
    public GameObject rat;
    public ProcGenDungeon map;
    public RockList rockList;
    public EnemyLists enemyLists;

    string currentScene;
    void Start() 
    {
        StartCoroutine(SetReferences());
    }

    IEnumerator SetReferences() {
        yield return new WaitForSeconds(1.5f);

        currentScene = SceneManager.GetActiveScene().name;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        itemsOnFloorList = GameObject.FindGameObjectWithTag("ItemsOnFloor").GetComponent<ItemsOnFloorList>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        challenges = GameObject.FindGameObjectWithTag("Challenges").GetComponent<ChallengeMenu>();
        if(currentScene == "CaveGameScene"){
        map = GameObject.FindGameObjectWithTag("map").GetComponent<ProcGenDungeon>();
        }
        rockList = GameObject.FindGameObjectWithTag("Environment").GetComponent<RockList>();
        enemyLists =  GameObject.FindGameObjectWithTag("Environment").GetComponent<EnemyLists>();

    }

    void Update() {
        currentScene = SceneManager.GetActiveScene().name;
    }
    public void SaveAll() 
    {
        if(currentScene == "CaveGameScene"){
        SaveMapSeed();
        }
        SavePlayer();
        SaveItemsOnFloor();
        SaveRocks();
        SaveEnemies();
        SaveInventory();
        SaveChallenges();

  

    }

    public void LoadAll()
    {

        if(currentScene == "CaveGameScene"){
        LoadMap();
        }
        LoadPlayer();
        LoadRocks();
        LoadEnemies();
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
        
        map.GenerateAll(true);

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
                newItem.name = itemData.name;
                newItem.GetComponent<UseDrop>().itemType = itemData.itemType;
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

            GameObject newRock = Instantiate(rock, position, Quaternion.identity, GameObject.Find("Environment").transform );
            rockList.rockList.Add(newRock);
        }


    }
    public void SaveEnemies()
    {
        SaveLoad.SaveEnemies(enemyLists);

    }

    public void LoadEnemies()
    {
        foreach(GameObject bat in enemyLists.batList){
            Destroy(bat);
        }
        foreach(GameObject worm in enemyLists.wormList){
            Destroy(worm);
        }
        foreach(GameObject rat in enemyLists.ratList){
            Destroy(rat);
        }


        SaveLoad.LoadEnemies(enemyLists);
        enemyLists.batList = new List<GameObject>();
        enemyLists.ratList = new List<GameObject>();
        enemyLists.wormList = new List<GameObject>();


        foreach(EnemyData batData in enemyLists.batDataList){
            Vector2 position;
            position.x = batData.position[0];
            position.y = batData.position[1];

            GameObject newBat = Instantiate(bat, position, Quaternion.identity, GameObject.Find("Environment").transform);
            
            
            newBat.transform.GetComponentInChildren<EnemyStats>().maxHealth = batData.maxHealth;
            newBat.transform.GetComponentInChildren<EnemyStats>().damage = batData.damage;
            newBat.transform.GetComponentInChildren<EnemyStats>().currentHealth = batData.currentHealth;
            newBat.transform.GetComponentInChildren<EnemyStats>().healthBar.SetStat(batData.currentHealth, batData.maxHealth);
            enemyLists.batList.Add(newBat);
        }
        foreach(EnemyData ratData in enemyLists.ratDataList){
            Vector2 position;
            position.x = ratData.position[0];
            position.y = ratData.position[1];

            GameObject newRat = Instantiate(rat, position, Quaternion.identity, GameObject.Find("Environment").transform);
            
            
            newRat.transform.GetComponentInChildren<EnemyStats>().maxHealth = ratData.maxHealth;
            newRat.transform.GetComponentInChildren<EnemyStats>().damage = ratData.damage;
            newRat.transform.GetComponentInChildren<EnemyStats>().currentHealth = ratData.currentHealth;
            newRat.transform.GetComponentInChildren<EnemyStats>().healthBar.SetStat(ratData.currentHealth, ratData.maxHealth);
            enemyLists.ratList.Add(newRat);
        }
        foreach(EnemyData wormData in enemyLists.wormDataList){
            Vector2 position;
            position.x = wormData.position[0];
            position.y = wormData.position[1];

            GameObject newWorm = Instantiate(worm, position, Quaternion.identity, GameObject.Find("Environment").transform);
            
            newWorm.transform.GetComponentInChildren<EnemyStats>().maxHealth = wormData.maxHealth;
            newWorm.transform.GetComponentInChildren<EnemyStats>().damage = wormData.damage;
            newWorm.transform.GetComponentInChildren<EnemyStats>().currentHealth = wormData.currentHealth;
            newWorm.transform.GetComponentInChildren<EnemyStats>().healthBar.SetStat(wormData.currentHealth,wormData.maxHealth);
            enemyLists.wormList.Add(newWorm);
        }
        enemyLists.batDataList = new List<EnemyData>();
        enemyLists.ratDataList = new List<EnemyData>();
        enemyLists.wormDataList = new List<EnemyData>();

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


            GameObject newItem = Instantiate(item, position, Quaternion.identity, GameObject.Find("Environment").transform);
            
            newItem.name = itemData.name;
            
            Texture2D spriteTexture = new Texture2D(itemData.spriteW, itemData.spriteH,TextureFormat.RGBA32, false );
            spriteTexture.LoadRawTextureData(itemData.spriteTex);
            spriteTexture.Apply();
            Sprite loadedSprite = Sprite.Create(spriteTexture, new Rect(0.0f,0.0f , itemData.spriteW, itemData.spriteH), Vector2.one);
            newItem.GetComponent<SpriteRenderer>().sprite = loadedSprite;
            
            item.GetComponent<Item>().itemSprite = loadedSprite;
            item.GetComponent<Item>().itemType = itemData.itemType;
            GameObject.FindGameObjectWithTag("ItemsOnFloor").GetComponent<ItemsOnFloorList>().itemList.Add(newItem);
        }
        itemsOnFloorList.itemDataList = new List<ItemData>();
    }


    public void SaveChallenges(){
        SaveLoad.SaveChallenges(challenges);

    }

    public void LoadChallenges(){
        SaveLoad.LoadChallenges(challenges);
    }


}
