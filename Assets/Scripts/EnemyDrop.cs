using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{

    public GameObject item;
    public Sprite itemSprite;
    public string itemName;
    public Item.ItemType itemType;

    private GameObject enemy;

    private void Start()
    {
        enemy = gameObject;
    }

    public void Drop()
    {
        if (Random.Range(0, 2) == 1)
        {
            Vector2 enemySpot = new Vector2(enemy.transform.position.x, enemy.transform.position.y);

            GameObject newItem = Instantiate(item, enemySpot, Quaternion.identity, GameObject.Find("Environment").transform);
            newItem.name = itemName;

            newItem.GetComponent<Item>().itemType = itemType;

            newItem.GetComponent<SpriteRenderer>().sprite = itemSprite;
            newItem.GetComponent<Item>().itemSprite = itemSprite;

            ItemsOnFloorList itemsOnFloorList = GameObject.FindGameObjectWithTag("ItemsOnFloor").GetComponent<ItemsOnFloorList>();
            itemsOnFloorList.itemDataList.Add(new ItemData(newItem.GetComponent<Item>()));
            itemsOnFloorList.itemList.Add(newItem);
        }
    }

}
