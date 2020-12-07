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
    public GameObject smallrock1;
    public GameObject smallrock2;
    public GameObject inventoryItem;
    public GameObject bat;
    public GameObject worm;
    public GameObject rat;
    public GameObject vampire;
    public GameObject skeleton;
    public GameObject zombie;
    public GameObject objectsToMove;
    public  GameObject objectsToMovePrefab;
    public ProcGenDungeon map;
    public RockList rockList;
    public EnemyLists enemyLists;
    public bool enemiesLoaded;
    private GameObject spriteAtlas;
    public Sprite nosword;
    public Sprite femaleSprite;

    public bool loadfrommenu;
    public int character;

    string currentScene;
    void Start() 
    {
        loadfrommenu = false;
        for(int i= 0; i < SceneManager.sceneCount; i++){
            if(SceneManager.GetSceneAt(i).name == "MainMenu"){
                loadfrommenu = true;
                
                Debug.Log("Loaded from menu");
            }
        }
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
        spriteAtlas = GameObject.Find("Sprite Atlas");
        if(loadfrommenu){
            
            // SceneManager.UnloadSceneAsync("MainMenu");
            LoadAll();
            
        } else {
            SaveAll();
        }
    }

    void Update() {
        currentScene = SceneManager.GetActiveScene().name;
    }
    public void SaveAll() 
    {
        SaveLoad.SaveMenuInfo();
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
        enemiesLoaded = GameObject.Find("LevelLoader").GetComponent<LevelLoader>().enemiesLoaded;
        LoadPlayer();
        LoadRocks();
        LoadEnemies();
        if(!enemiesLoaded){
          LoadItemsOnFloor();
        }
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
        
        
        player.healthBar.SetMaxStat( player.overallHealth);
        player.healthBar.SetStat(player.currentHealth, player.overallHealth);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ChangeSkin>().updateSkin();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().swordEquipped = player.swordEquipped;
        Sprite swordSprite;
        switch(player.sword){
            case "Swords_Copper":
                swordSprite =  spriteAtlas.GetComponent<SpriteAtlas>().copperSword;
                GameObject.FindGameObjectWithTag("Player").GetComponent<ChangeSkin>().CopperSkin();
                break;
            case "Swords_Silver":
                swordSprite =  spriteAtlas.GetComponent<SpriteAtlas>().silverSword;
                GameObject.FindGameObjectWithTag("Player").GetComponent<ChangeSkin>().SilverSkin();
                break;
            case "Swords_Iron":
                swordSprite =  spriteAtlas.GetComponent<SpriteAtlas>().ironSword;
                GameObject.FindGameObjectWithTag("Player").GetComponent<ChangeSkin>().IronSkin();
                break;
            case "Swords_Gold":
                swordSprite =  spriteAtlas.GetComponent<SpriteAtlas>().goldSword;
                GameObject.FindGameObjectWithTag("Player").GetComponent<ChangeSkin>().GoldSkin();
                break;
            case "Swords_Obsidian":
                swordSprite =  spriteAtlas.GetComponent<SpriteAtlas>().obsidianSword;
                GameObject.FindGameObjectWithTag("Player").GetComponent<ChangeSkin>().ObsidianSkin();
                break;
            default:
                swordSprite = nosword;
                Debug.Log("Set to not having a sword");
                break;
        } 
        
        var image = GameObject.Find("Equipped Sword").transform.Find("Image");

        image.GetComponent<Image>().sprite = swordSprite;
        image.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        image.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, 0, 0);
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
        Destroy(map.returnExit());
        SaveLoad.LoadMapSeed(map);
        
        map.createdObjects = new List<GameObject>();
        map.GenerateAll(true);
        map.respawnExit();

        foreach(GameObject createdObject in map.createdObjects){
            Destroy(createdObject);
        }
        GameObject.Find("GridRescan").GetComponent<ScanGrid>().scan();
        
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
                Sprite loadedSprite = Sprite.Create(spriteTexture, new Rect(0.0f,0.0f , itemData.spriteW, itemData.spriteH), new Vector2(0.5f, 0.5f));
                newItem.GetComponent<Image>().sprite = loadedSprite;
                inventory.inventoryItems[i] = newItem;
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
        foreach(GameObject smallrockone in  rockList.smallRockOneList){
            
            Destroy(smallrockone);
            
        } 

        foreach(GameObject smallrocktwo in  rockList.smallrockTwoList){
            
            Destroy(smallrocktwo);
            
        } 
        SaveLoad.LoadRocks(rockList);
        rockList.rockList = new List<GameObject>();
        rockList.rockList.Clear();
        rockList.smallRockOneList.Clear();
        rockList.smallrockTwoList.Clear();

        foreach(RockData rockData in rockList.rockDataList){
            Vector2 position;
            position.x = rockData.position[0];
            position.y = rockData.position[1];

            GameObject newRock = Instantiate(rock, position, Quaternion.identity, GameObject.Find("Environment").transform );
            rockList.rockList.Add(newRock);
        }
        foreach(RockData rockData in rockList.smallRockOneDataList){
            Vector2 position;
            position.x = rockData.position[0];
            position.y = rockData.position[1];

            GameObject newRock = Instantiate(smallrock1, position, Quaternion.identity, GameObject.Find("Environment").transform );
            rockList.smallRockOneList.Add(newRock);
        }
        foreach(RockData rockData in rockList.smallRockTwoDataList){
            Vector2 position;
            position.x = rockData.position[0];
            position.y = rockData.position[1];

            GameObject newRock = Instantiate(smallrock2, position, Quaternion.identity, GameObject.Find("Environment").transform );
            rockList.smallrockTwoList.Add(newRock);
        }
        rockList.rockDataList.Clear();
        rockList.smallRockTwoDataList.Clear();
        rockList.smallRockOneDataList.Clear();

    }
    public void SaveEnemies()
    {
        SaveLoad.SaveEnemies(enemyLists);

    }

    public void LoadEnemies()
    {
        bool tutorial;
        bool bossRoom;
        if(SceneManager.GetActiveScene().name == "ExitRoomScene"){
            tutorial = false;
            bossRoom = true;
        } else if (SceneManager.GetActiveScene().name == "TutorialScene"){
            tutorial = true;
            bossRoom = false;
        } else {
            tutorial = false;
            bossRoom = false;
        }
        foreach(GameObject bat in enemyLists.batList){
            Destroy(bat);
        }
        foreach(GameObject worm in enemyLists.wormList){
            Destroy(worm);
        }
        foreach(GameObject rat in enemyLists.ratList){
            Destroy(rat);
        }
        foreach(GameObject zomb in enemyLists.zombList){
            Destroy(zomb);
        }
        foreach(GameObject vamp in enemyLists.vampList){
            Destroy(vamp);
        }
        foreach(GameObject skel in enemyLists.skelList){
            Destroy(skel);
        }


        SaveLoad.LoadEnemies(enemyLists);
        enemyLists.batList = new List<GameObject>();
        enemyLists.ratList = new List<GameObject>();
        enemyLists.wormList = new List<GameObject>();
        enemyLists.vampList = new List<GameObject>();
        enemyLists.zombList = new List<GameObject>();
        enemyLists.skelList = new List<GameObject>();


        foreach(EnemyData batData in enemyLists.batDataList){
            Vector2 position;
            position.x = batData.position[0];
            position.y = batData.position[1];

            GameObject newBat = Instantiate(bat, position, Quaternion.identity, GameObject.Find("Environment").transform);
            
            
            newBat.transform.GetComponentInChildren<EnemyStats>().maxHealth = batData.maxHealth;
            newBat.transform.GetComponentInChildren<EnemyStats>().damage = batData.damage;
            newBat.transform.GetComponentInChildren<EnemyStats>().currentHealth = batData.currentHealth;
            newBat.transform.GetComponentInChildren<EnemyStats>().healthBar.SetStat(batData.currentHealth, batData.maxHealth);
            newBat.transform.GetComponentInChildren<EnemyStats>().isBoss = false;
            newBat.transform.GetComponentInChildren<EnemyStats>().enemyName = "Bat";
            newBat.transform.GetComponentInChildren<EnemyDrop>().bossRoom = bossRoom;
            newBat.transform.GetComponentInChildren<EnemyDrop>().tutorial = tutorial;
            newBat.name = "Bat";
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
            newRat.transform.GetComponentInChildren<EnemyStats>().isBoss = false;
            newRat.transform.GetComponentInChildren<EnemyStats>().enemyName = "Rat";
            newRat.transform.GetComponentInChildren<EnemyDrop>().bossRoom = bossRoom;
            newRat.transform.GetComponentInChildren<EnemyDrop>().tutorial = tutorial;
            newRat.name = "Rat";
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
            newWorm.transform.GetComponentInChildren<EnemyStats>().isBoss = false;
            newWorm.transform.GetComponentInChildren<EnemyStats>().enemyName = "Worm";
            newWorm.transform.GetComponentInChildren<EnemyDrop>().bossRoom = bossRoom;
            newWorm.transform.GetComponentInChildren<EnemyDrop>().tutorial = tutorial;
            newWorm.name = "Worm";
            enemyLists.wormList.Add(newWorm);
        }
        foreach(EnemyData vampData in enemyLists.vampDataList){
            Vector2 position;
            position.x = vampData.position[0];
            position.y = vampData.position[1];

            GameObject newVamp = Instantiate(vampire, position, Quaternion.identity, GameObject.Find("Environment").transform);
            
            
            newVamp.transform.GetComponentInChildren<EnemyStats>().maxHealth = vampData.maxHealth;
            newVamp.transform.GetComponentInChildren<EnemyStats>().damage = vampData.damage;
            newVamp.transform.GetComponentInChildren<EnemyStats>().currentHealth = vampData.currentHealth;
            newVamp.transform.GetComponentInChildren<EnemyStats>().healthBar.SetStat(vampData.currentHealth, vampData.maxHealth);
            newVamp.transform.GetComponentInChildren<EnemyStats>().isBoss = true;
            newVamp.transform.GetComponentInChildren<EnemyStats>().enemyName = "Vampire";
            newVamp.transform.GetComponentInChildren<EnemyDrop>().bossRoom = bossRoom;
            newVamp.transform.GetComponentInChildren<EnemyDrop>().tutorial = tutorial;
            newVamp.name = "Vampire";
            enemyLists.vampList.Add(newVamp);
        }
        foreach(EnemyData zombData in enemyLists.zombDataList){
            Vector2 position;
            position.x = zombData.position[0];
            position.y = zombData.position[1];

            GameObject newZomb = Instantiate(zombie, position, Quaternion.identity, GameObject.Find("Environment").transform);
            
            
            newZomb.transform.GetComponentInChildren<EnemyStats>().maxHealth = zombData.maxHealth;
            newZomb.transform.GetComponentInChildren<EnemyStats>().damage = zombData.damage;
            newZomb.transform.GetComponentInChildren<EnemyStats>().currentHealth = zombData.currentHealth;
            newZomb.transform.GetComponentInChildren<EnemyStats>().healthBar.SetStat(zombData.currentHealth, zombData.maxHealth);
            newZomb.transform.GetComponentInChildren<EnemyStats>().isBoss = true;
            newZomb.transform.GetComponentInChildren<EnemyStats>().enemyName = "zombie";
            newZomb.transform.GetComponentInChildren<EnemyDrop>().bossRoom = bossRoom;
            newZomb.transform.GetComponentInChildren<EnemyDrop>().tutorial = tutorial;
            newZomb.name = "zombie";
            enemyLists.zombList.Add(newZomb);
        }
        foreach(EnemyData skelData in enemyLists.skelDataList){
            Vector2 position;
            position.x = skelData.position[0];
            position.y = skelData.position[1];

            GameObject newSkel = Instantiate(skeleton, position, Quaternion.identity, GameObject.Find("Environment").transform);
            
            newSkel.transform.GetComponentInChildren<EnemyStats>().maxHealth = skelData.maxHealth;
            newSkel.transform.GetComponentInChildren<EnemyStats>().damage = skelData.damage;
            newSkel.transform.GetComponentInChildren<EnemyStats>().currentHealth = skelData.currentHealth;
            newSkel.transform.GetComponentInChildren<EnemyStats>().healthBar.SetStat(skelData.currentHealth,skelData.maxHealth);
            newSkel.transform.GetComponentInChildren<EnemyStats>().isBoss = true;
            newSkel.transform.GetComponentInChildren<EnemyStats>().enemyName = "Skeleton";
            newSkel.transform.GetComponentInChildren<EnemyDrop>().bossRoom = bossRoom;
            newSkel.transform.GetComponentInChildren<EnemyDrop>().tutorial = tutorial;
            newSkel.name = "Skeleton";
            enemyLists.skelList.Add(newSkel);
        }
        enemyLists.batDataList = new List<EnemyData>();
        enemyLists.ratDataList = new List<EnemyData>();
        enemyLists.wormDataList = new List<EnemyData>();
        enemyLists.skelDataList = new List<EnemyData>();
        enemyLists.vampDataList = new List<EnemyData>();
        enemyLists.zombDataList = new List<EnemyData>();
        enemiesLoaded = true;

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
            Sprite loadedSprite = Sprite.Create(spriteTexture, new Rect(0.0f,0.0f , itemData.spriteW, itemData.spriteH), new Vector2(0.5f, 0.5f));
            newItem.GetComponent<SpriteRenderer>().sprite = loadedSprite;
            
            item.GetComponent<Item>().itemSprite = loadedSprite;
            item.GetComponent<Item>().itemType = itemData.itemType;
            item.GetComponent<Item>().inventory = inventory;
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
