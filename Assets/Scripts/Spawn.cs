﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject item;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnDroppedItem(string name, Sprite sprite)
    {
        Vector2 playerPos = new Vector2(player.position.x, player.position.y + 2);
        GameObject newItem = Instantiate(item, playerPos, Quaternion.identity, GameObject.Find("Environment").transform);

        newItem.name = name;

        newItem.GetComponent<SpriteRenderer>().sprite = sprite;
        newItem.GetComponent<Item>().itemSprite = sprite;

        ItemsOnFloorList itemsOnFloorList = GameObject.FindGameObjectWithTag("ItemsOnFloor").GetComponent<ItemsOnFloorList>();
        
        itemsOnFloorList.itemList.Add(newItem);
    }
}
