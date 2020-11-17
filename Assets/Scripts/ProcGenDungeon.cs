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

    public GameObject exitPrefab;

    public GameObject[] objects;
    public List<GameObject> createdObjects = new List<GameObject>();

    public Sprite[] fruitSprites;

    public SpriteAtlas spriteAtlas;

    public static int caveLevel = 1;

    private List<Vector3Int> spawnLocations = new List<Vector3Int>();
    public int seed;
 
    public bool onload;
    
    

    private int routeCount;

    public void Start()
    {
        seed = Random.Range(1, 10000000);
        onload = false;
        GenerateAll(onload);
    }

    public void GenerateAll(bool onload) {
        StartCoroutine(Generate(onload));
    }

    IEnumerator Generate(bool onload){
        yield return new WaitForSeconds(1);

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
            int rand = Random.Range(0, objects.Length);

            GameObject newObject = Instantiate(objects[rand], spawnLocations[i], Quaternion.identity, GameObject.Find("Environment").transform);

            createdObjects.Add(newObject);
            if(rand == 0){
                GameObject.FindGameObjectWithTag("Environment").GetComponent<RockList>().rockList.Add(newObject);
                GameObject.FindGameObjectWithTag("Environment").GetComponent<RockList>().rockDataList.Add(new RockData(newObject));

            } else if(rand == 1) {
                int barOrFruitRand = Random.Range(0, 2);

                if(barOrFruitRand == 0 ){
                    SpawnFruit(newObject);
                } else {
                    SpawnBars(newObject);
                }
            } else if(rand == 2) {
                GameObject.FindGameObjectWithTag("Environment").GetComponent<EnemyLists>().batList.Add(newObject);
            }
        }
    }

    private void SpawnFruit(GameObject newObject) {
        int rand = Random.Range(0, 8);

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

    private void SpawnBars(GameObject newObject) {
        // int rand = Random.RandomRange(0, 4);
        int rand  = 0;

        switch (rand) {
            case 0:
                newObject.name = "Copper Bar";
                break;
            case 1:
                newObject.name = "Iron Bar";
                break;
            case 2:
                newObject.name = "Silver Bar";
                break;
            case 3:
                newObject.name = "Gold Bar";
                break;
            case 4:
                newObject.name = "Obsidian Bar";
                break;
        }
        
        newObject.GetComponent<SpriteRenderer>().sprite = spriteAtlas.copperBar;
        newObject.GetComponent<Item>().itemSprite = spriteAtlas.copperBar;
        newObject.GetComponent<Item>().itemType = Item.ItemType.CopperBar;
        ItemsOnFloorList itemLists = GameObject.FindGameObjectWithTag("ItemsOnFloor").GetComponent<ItemsOnFloorList>();
        itemLists.itemList.Add(newObject);
    }
}