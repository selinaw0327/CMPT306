using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProcGenDungeon : MonoBehaviour
{

    [SerializeField]
    private Tile groundTile;
    [SerializeField]
    private Tile pitTile;
    [SerializeField]
    private Tile topWallTile;
    [SerializeField]
    private Tile botWallTile;
    [SerializeField]
    public Tilemap groundMap;
    [SerializeField]
    public Tilemap pitMap;
    [SerializeField]
    public Tilemap wallMap;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private int deviationRate = 10;
    [SerializeField]
    private int roomRate = 15;
    [SerializeField]
    private int maxRouteLength;
    [SerializeField]
    private int maxRoutes = 20;

    private int routeCount;

    public static int caveLevel = 0;

    public GameObject exitPrefab;

    public GameObject[] rockPrefabs;
    public GameObject[] enemyPrefabs;
    public GameObject itemPrefab;
    public List<GameObject> createdObjects = new List<GameObject>();

    private List<Vector3Int> spawnLocations = new List<Vector3Int>();

    public Sprite[] fruitSprites;
    public SpriteAtlas spriteAtlas;

    public int seed;
    public bool onload;

    public void Start()
    {
        // Set Size of cave depending on level
        // switch(caveLevel) {
        //     case 0:
        //         maxRouteLength = 100;
        //         break;
        //     case 1:
        //         maxRouteLength = 150;
        //         break;
        //     case 2:
        //         maxRouteLength = 200;
        //         break;
        // }

        seed = Random.Range(1, 10000000);
        onload = false;
        GenerateAll(onload);
    }

    public void GenerateAll(bool onload) {
        StartCoroutine(Generate(onload));
    }

    IEnumerator Generate(bool onload){
        Random.seed = seed;
        int x = 0;
        int y = 0;
        int routeLength = 0;
        routeCount = 0;
        GenerateSquare(x, y, 1);
        Vector2Int previousPos = new Vector2Int(x, y);
        y += 3;
        GenerateSquare(x, y, 1);
        NewRoute(x, y, routeLength, previousPos);

        FillWalls();

        yield return new WaitForSeconds(1);

        if(!onload){
            Debug.Log("not on load");
            FillSpawnLocations();
        }
    }

    private void FillWalls()
    {
        Vector3Int lastWall = new Vector3Int(0,0,0);
        BoundsInt bounds = groundMap.cellBounds;
        for (int xMap = bounds.xMin - 10; xMap <= bounds.xMax + 10; xMap++)
        {
            for (int yMap = bounds.yMin - 10; yMap <= bounds.yMax + 10; yMap++)
            {
                Vector3Int pos = new Vector3Int(xMap, yMap, 0);
                Vector3Int posBelow = new Vector3Int(xMap, yMap - 1, 0);
                Vector3Int posAbove = new Vector3Int(xMap, yMap + 1, 0);
                TileBase tile = groundMap.GetTile(pos);
                TileBase tileBelow = groundMap.GetTile(posBelow);
                TileBase tileAbove = groundMap.GetTile(posAbove);
                if (tile == null)
                {
                    pitMap.SetTile(pos, pitTile);
                    if (tileBelow != null)
                    {
                        wallMap.SetTile(pos, topWallTile);

                        // Find position of wall tile farthest from the player for exit
                        int absPos = Mathf.Abs(pos.x) + Mathf.Abs(pos.y);
                        int absLastWall = Mathf.Abs(lastWall.x) + Mathf.Abs(lastWall.y);
                        if(absPos > absLastWall) {
                            lastWall = pos;
                        }                        
                    }
                    else if (tileAbove != null)
                    {
                        wallMap.SetTile(pos, botWallTile);
                    }
                }
            }
        }
        Vector3Int posRight = new Vector3Int(lastWall.x+1, lastWall.y, 0);
        Vector3Int posLeft = new Vector3Int(lastWall.x-1, lastWall.y, 0);

        TileBase wallTileRight = wallMap.GetTile(posRight);
        TileBase wallTileLeft = wallMap.GetTile(posLeft);

        Vector3 lastWallF = lastWall;

        if(wallTileRight == null) {
            lastWallF.x -= 0.55f;
        }
        else {
            lastWallF.x += 0.5f;
        }
        lastWallF.y += 0.5f;
        GameObject exit = Instantiate(exitPrefab, lastWallF, Quaternion.identity, GameObject.Find("Environment").transform);
        exit.name = "Exit";
    }

    private void NewRoute(int x, int y, int routeLength, Vector2Int previousPos)
    {
        if (routeCount < maxRoutes)
        {
            routeCount++;
            while (++routeLength < maxRouteLength)
            {
                //Initialize
                bool routeUsed = false;
                int xOffset = x - previousPos.x; //0
                int yOffset = y - previousPos.y; //3
                int roomSize = 1; //Hallway size
                if (Random.Range(1, 100) <= roomRate)
                    roomSize = Random.Range(3, 6);
                previousPos = new Vector2Int(x, y);

                //Go Straight
                if (Random.Range(1, 100) <= deviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x + xOffset, previousPos.y + yOffset, roomSize);
                        NewRoute(previousPos.x + xOffset, previousPos.y + yOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else
                    {
                        x = previousPos.x + xOffset;
                        y = previousPos.y + yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }

                //Go left
                if (Random.Range(1, 100) <= deviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x - yOffset, previousPos.y + xOffset, roomSize);
                        NewRoute(previousPos.x - yOffset, previousPos.y + xOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else
                    {
                        y = previousPos.y + xOffset;
                        x = previousPos.x - yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }
                //Go right
                if (Random.Range(1, 100) <= deviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x + yOffset, previousPos.y - xOffset, roomSize);
                        NewRoute(previousPos.x + yOffset, previousPos.y - xOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else
                    {
                        y = previousPos.y - xOffset;
                        x = previousPos.x + yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }

                if (!routeUsed)
                {
                    x = previousPos.x + xOffset;
                    y = previousPos.y + yOffset;
                    GenerateSquare(x, y, roomSize);
                }
            }
        }
    }

    private void GenerateSquare(int x, int y, int radius)
    {
        // Create spawn points for objects and items
        if(routeCount > 1) {    // Check if the player is in the first room
            if(Random.Range(0, 3) == 1) {   // Random chance to create a spawn point
                Vector3Int location = new Vector3Int(x, y, 0);
                if(!spawnLocations.Contains(location)) {    // Check if the location is already a spawn point
                    spawnLocations.Add(location);
                }
            }
        }
        for (int tileX = x - radius; tileX <= x + radius; tileX++)
        {
            for (int tileY = y - radius; tileY <= y + radius; tileY++)
            {
                Vector3Int tilePos = new Vector3Int(tileX, tileY, 0);
                groundMap.SetTile(tilePos, groundTile);
            }
        }
    }


    private void FillSpawnLocations() {
        for(int i = 0; i < spawnLocations.Count; i++) {
            Debug.Log(spawnLocations.Count);
            int rand = Random.Range(0, 100);

            if(rand < 50) { // 50% chance to spawn rocks
                SpawnRocks(spawnLocations[i]);
            }
            else if(rand < 85) { // 35% chance to spawn enemies
                SpawnEnemies(spawnLocations[i]);
            }
            else { // 15% chance to spawn fruit
                SpawnFruit(spawnLocations[i]);
            }    
        }
    }

    // Randomly select on the rock refabs
    private void SpawnRocks(Vector3 spawnLocation) {
        int rand = Random.Range(0, rockPrefabs.Length);
        
        GameObject newObject = Instantiate(rockPrefabs[rand], spawnLocation, Quaternion.identity, GameObject.Find("Environment").transform);
        createdObjects.Add(newObject);
        
        switch(rockPrefabs[rand].name) {
            case "Large Rock":
                GameObject.FindGameObjectWithTag("Environment").GetComponent<RockList>().rockList.Add(newObject);
                GameObject.FindGameObjectWithTag("Environment").GetComponent<RockList>().rockDataList.Add(new RockData(newObject));
                break;
            case "Small Rock One":
                GameObject.FindGameObjectWithTag("Environment").GetComponent<RockList>().smallRockOneList.Add(newObject);
                GameObject.FindGameObjectWithTag("Environment").GetComponent<RockList>().smallRockOneDataList.Add(new RockData(newObject));
                break;
            case "Small Rock Two":
                GameObject.FindGameObjectWithTag("Environment").GetComponent<RockList>().smallrockTwoList.Add(newObject);
                GameObject.FindGameObjectWithTag("Environment").GetComponent<RockList>().smallRockTwoDataList.Add(new RockData(newObject));
                break;
        }

        newObject.name = rockPrefabs[rand].name;
    }

    // Choose an enemy to spawn depending on the level of the cave
    // First Level: Worms
    // Second Level: Worms and Rats
    // Third Level: Worms, Rats, and Bats
    private void SpawnEnemies(Vector3 spawnLocation) {
        int rand = Random.Range(0 , caveLevel + 1);
        GameObject newObject;
                
        newObject = Instantiate(enemyPrefabs[rand], spawnLocation, Quaternion.identity, GameObject.Find("Environment").transform);
        createdObjects.Add(newObject);
        EnemyLists enemyLists =  GameObject.FindGameObjectWithTag("Environment").GetComponent<EnemyLists>();
        switch(enemyPrefabs[rand].name) {
            case "Worm":
                GameObject.FindGameObjectWithTag("Environment").GetComponent<EnemyLists>().wormList.Add(newObject);
                enemyLists.wormList.Add(newObject);
                break;
            case "Rat":
                GameObject.FindGameObjectWithTag("Environment").GetComponent<EnemyLists>().ratList.Add(newObject);
                enemyLists.ratList.Add(newObject);
                break;
            case "Bat":
                GameObject.FindGameObjectWithTag("Environment").GetComponent<EnemyLists>().batList.Add(newObject);
                enemyLists.batList.Add(newObject);
                break;
        }

        newObject.name = enemyPrefabs[rand].name;
    }

    // Randomly select a fruit to spawn
    private void SpawnFruit(Vector3 spawnLocation) {
        int rand = Random.Range(0, fruitSprites.Length);

        GameObject newObject = Instantiate(itemPrefab, spawnLocation, Quaternion.identity, GameObject.Find("Environment").transform);
        createdObjects.Add(newObject);

        switch (rand) {
            case 0:
                newObject.name = "Banana";
                break;
            case 1:
                newObject.name = "Cherries";
                break;
            case 2:
                newObject.name = "Kiwi";
                break;
            case 3:
                newObject.name = "Melon";
                break;
            case 4:
                newObject.name = "Orange";
                break;
            case 5:
                newObject.name = "Pineapple";
                break;
            case 6:
                newObject.name = "Strawberry";
                break;
            case 7: 
                newObject.name = "Apple";
                break;
        }        
        newObject.GetComponent<SpriteRenderer>().sprite = fruitSprites[rand];
        newObject.GetComponent<Item>().itemSprite = fruitSprites[rand];

        ItemsOnFloorList itemLists = GameObject.FindGameObjectWithTag("ItemsOnFloor").GetComponent<ItemsOnFloorList>();
        itemLists.itemList.Add(newObject);
    }

    // private void SpawnBars(GameObject newObject) {
    //     // int rand = Random.RandomRange(0, 4);
    //     int rand  = 0;

    //     switch (rand) {
    //         case 0:
    //             newObject.name = "Copper Bar";
    //             break;
    //         case 1:
    //             newObject.name = "Iron Bar";
    //             break;
    //         case 2:
    //             newObject.name = "Silver Bar";
    //             break;
    //         case 3:
    //             newObject.name = "Gold Bar";
    //             break;
    //         case 4:
    //             newObject.name = "Obsidian Bar";
    //             break;
    //     }
        
    //     newObject.GetComponent<SpriteRenderer>().sprite = spriteAtlas.copperBar;
    //     newObject.GetComponent<Item>().itemSprite = spriteAtlas.copperBar;
    //     newObject.GetComponent<Item>().itemType = Item.ItemType.CopperBar;
    //     ItemsOnFloorList itemLists = GameObject.FindGameObjectWithTag("ItemsOnFloor").GetComponent<ItemsOnFloorList>();
    //     itemLists.itemList.Add(newObject);
    // }
}