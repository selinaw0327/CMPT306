﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseDrop : MonoBehaviour
{
    public GameObject item;
    private Inventory inventory;
    private GameObject slot;
    public Sprite sprite;

    private GameObject spriteAtlas;

    private int itemIndex;

    public Item.ItemType itemType;

    private ChangeSkin changeSkin;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        slot = transform.parent.gameObject;
        sprite = gameObject.GetComponent<Image>().sprite;
        spriteAtlas = GameObject.Find("Sprite Atlas");

        itemIndex = inventory.IndexOf(name);

        changeSkin = GameObject.FindGameObjectWithTag("Player").GetComponent<ChangeSkin>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log("Item Type: " + itemType.ToString());

        switch (itemType)
        {
            case Item.ItemType.Fruit:
                player.GetComponent<PlayerStats>().TakeHunger(-10);
                player.GetComponent<PlayerStats>().Heal(10);
                UpdateQuantity(-1);
                break;
            // BARS
            case Item.ItemType.CopperBar:
                if (inventory.quantity[itemIndex] < 10)
                {
                    Debug.Log("You need 10 copper bars to forge a copper sword");
                }
                else if (inventory.quantity[itemIndex] >= 10)
                {
                    UpdateQuantity(-10);
                    Sprite copperSword = spriteAtlas.GetComponent<SpriteAtlas>().copperSword;
                    Forge("Copper Sword", Item.ItemType.CopperSword, copperSword);
                }
                break;
            case Item.ItemType.SilverBar:
                if (inventory.quantity[itemIndex] < 10)
                {
                    Debug.Log("You need 10 silver bars to forge a silver sword");
                } else if (inventory.quantity[itemIndex] >= 10)
                {
                    UpdateQuantity(-10);
                    Sprite silverSword = spriteAtlas.GetComponent<SpriteAtlas>().silverSword;
                    Forge("Silver Sword", Item.ItemType.SilverSword, silverSword);
                }
                break;
            case Item.ItemType.IronBar:
                if (inventory.quantity[itemIndex] < 10)
                {
                    Debug.Log("You need 10 iron bars to forge a iron sword");
                }
                else if (inventory.quantity[itemIndex] >= 10)
                {
                    UpdateQuantity(-10);
                    Sprite ironSword = spriteAtlas.GetComponent<SpriteAtlas>().ironSword;
                    Forge("Iron Sword", Item.ItemType.IronSword, ironSword);
                }
                break;
            case Item.ItemType.GoldBar:
                if (inventory.quantity[itemIndex] < 10)
                {
                    Debug.Log("You need 10 gold bars to forge a gold sword");
                }
                else if (inventory.quantity[itemIndex] >= 10)
                {
                    UpdateQuantity(-10);
                    Sprite goldSword = spriteAtlas.GetComponent<SpriteAtlas>().goldSword;
                    Forge("Gold Sword", Item.ItemType.GoldSword, goldSword);
                }
                break;
            case Item.ItemType.ObsidianBar:
                if (inventory.quantity[itemIndex] < 10)
                {
                    Debug.Log("You need 10 obsidian bars to forge a obsidian sword");
                }
                else if (inventory.quantity[itemIndex] >= 10)
                {
                    UpdateQuantity(-10);
                    Sprite obsidianSword = spriteAtlas.GetComponent<SpriteAtlas>().obsidianSword;
                    Forge("Obsidian Sword", Item.ItemType.ObsidianSword, obsidianSword);
                }
                break;
            // SWORDS
            case Item.ItemType.CopperSword:
                // Code for what happens when Copper sword is right-clicked in inventory
                Equip(spriteAtlas.GetComponent<SpriteAtlas>().copperSword);
                changeSkin.CopperSkin();
                break;
            case Item.ItemType.SilverSword:
                // Code for what happens when Silver sword is right-clicked in inventory
                Equip(spriteAtlas.GetComponent<SpriteAtlas>().silverSword);
                changeSkin.SilverSkin();
                break;
            case Item.ItemType.IronSword:
                // Code for what happens when Iron sword is right-clicked in 
                Equip(spriteAtlas.GetComponent<SpriteAtlas>().ironSword);
                changeSkin.IronSkin();
                break;
            case Item.ItemType.GoldSword:
                // Code for what happens when Gold sword is right-clicked in inventory
                Equip(spriteAtlas.GetComponent<SpriteAtlas>().goldSword);
                changeSkin.GoldSkin();
                break;
            case Item.ItemType.ObsidianSword:
                // Code for what happens when Obsidian sword is right-clicked in inventory
                Equip(spriteAtlas.GetComponent<SpriteAtlas>().obsidianSword);
                changeSkin.ObsidianSkin();
                break;
            default:
                break;
        }
        //UpdateQuantity(-1);
    }

    public void Forge(string name, Item.ItemType itemType, Sprite forgedSprite)
    {
        GetComponent<Spawn>().SpawnDroppedItem(name, itemType, forgedSprite);
    }

    public void Equip(Sprite swordSprite)
    {
        var image = GameObject.Find("Equipped Sword").transform.Find("Image");

        image.GetComponent<Image>().sprite = swordSprite;
        image.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        image.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, 0, 0);

        UpdateQuantity(-1);
    }

    public void Drop()
    {
        GetComponent<Spawn>().SpawnDroppedItem(name, itemType, sprite);
        UpdateQuantity(-1);
    }

    private void UpdateQuantity(int i)
    {
        
        inventory.quantity[itemIndex] += i;

        //Debug.Log(inventory.quantity[itemIndex] + ", " + i);

        // if the quantity is 0
        if (inventory.quantity[itemIndex] == 0)
        {
            Destroy(gameObject);
            inventory.items[itemIndex] = "";
            inventory.itemDataArr[inventory.IndexOf(slot)] = null;
            inventory.occupied[inventory.IndexOf(slot)] = false;
            inventory.inventoryItems[inventory.IndexOf(slot)] = null;
        }
    }
}
