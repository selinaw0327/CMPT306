using System.Collections;
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

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
        slot = transform.parent.gameObject;
        sprite = gameObject.GetComponent<Image>().sprite;
        spriteAtlas = GameObject.Find("Sprite Atlas");

        itemIndex = inventory.IndexOf(name);

        changeSkin = player.GetComponent<ChangeSkin>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use()
    {
       
        switch (itemType)
        {
            case Item.ItemType.Fruit:
                player.GetComponent<PlayerStats>().Heal(10);
                Debug.Log(player.GetComponent<PlayerStats>().currentHealth);
                UpdateQuantity(-1);
                break;
            case Item.ItemType.Bread:
                player.GetComponent<PlayerStats>().Heal(30);
                Debug.Log(player.GetComponent<PlayerStats>().currentHealth);
                UpdateQuantity(-1);
                break;
            case Item.ItemType.Steak:
                player.GetComponent<PlayerStats>().Heal(50);
                Debug.Log(player.GetComponent<PlayerStats>().currentHealth);
                UpdateQuantity(-1);
                break;
            // BARS
            case Item.ItemType.CopperBar:
                if (inventory.quantity[itemIndex] < 10)
                {
                    GameObject.Find("Insufficient Bars Alert").GetComponent<DialogueTrigger>().TriggerDialogue();
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
                    GameObject.Find("Insufficient Bars Alert").GetComponent<DialogueTrigger>().TriggerDialogue();
                }
                else if (inventory.quantity[itemIndex] >= 10)
                {
                    UpdateQuantity(-10);
                    Sprite silverSword = spriteAtlas.GetComponent<SpriteAtlas>().silverSword;
                    Forge("Silver Sword", Item.ItemType.SilverSword, silverSword);
                }
                break;
            case Item.ItemType.IronBar:
                if (inventory.quantity[itemIndex] < 10)
                {
                    GameObject.Find("Insufficient Bars Alert").GetComponent<DialogueTrigger>().TriggerDialogue();
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
                    GameObject.Find("Insufficient Bars Alert").GetComponent<DialogueTrigger>().TriggerDialogue();
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
                    GameObject.Find("Insufficient Bars Alert").GetComponent<DialogueTrigger>().TriggerDialogue();
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
                Equip(spriteAtlas.GetComponent<SpriteAtlas>().copperSword, player);
                changeSkin.CopperSkin();
                player.GetComponent<PlayerStats>().SetAdditionalHealth(50); // this is for testing only.
                break;
            case Item.ItemType.SilverSword:
                // Code for what happens when Silver sword is right-clicked in inventory
                Equip(spriteAtlas.GetComponent<SpriteAtlas>().silverSword, player);
                changeSkin.SilverSkin();
                break;
            case Item.ItemType.IronSword:
                // Code for what happens when Iron sword is right-clicked in 
                Equip(spriteAtlas.GetComponent<SpriteAtlas>().ironSword, player);
                changeSkin.IronSkin();
                player.GetComponent<PlayerStats>().SetAdditionalHealth(50);
                player.GetComponent<PlayerStats>().SetAdditionalDamage(25);
                break;
            case Item.ItemType.GoldSword:
                // Code for what happens when Gold sword is right-clicked in inventory
                Equip(spriteAtlas.GetComponent<SpriteAtlas>().goldSword, player);
                changeSkin.GoldSkin();
                player.GetComponent<PlayerStats>().SetAdditionalHealth(100);
                player.GetComponent<PlayerStats>().SetAdditionalDamage(50);
                break;
            case Item.ItemType.ObsidianSword:
                // Code for what happens when Obsidian sword is right-clicked in inventory
                Equip(spriteAtlas.GetComponent<SpriteAtlas>().obsidianSword, player);
                changeSkin.ObsidianSkin();
                player.GetComponent<PlayerStats>().SetAdditionalHealth(200);
                player.GetComponent<PlayerStats>().SetAdditionalDamage(100);
                break;
            default:
                break;
        }
        //UpdateQuantity(-1);
    }

    public void Forge(string name, Item.ItemType itemType, Sprite forgedSprite)
    {
        GetComponent<Spawn>().SpawnDroppedItem(name, itemType, forgedSprite);

        // trigger for how to equip sword tutorial
        if (!player.GetComponent<Equipped>().equipTutorial)
        {
            GameObject.Find("Equip Copper Sword").GetComponent<DialogueTrigger>().TriggerDialogue();
            player.GetComponent<Equipped>().equipTutorial = true;
        }
    }

    public void Equip(Sprite swordSprite, GameObject player)
    {
        var image = GameObject.Find("Equipped Sword").transform.Find("Image");

        image.GetComponent<Image>().sprite = swordSprite;
        image.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        image.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, 0, 0);

        player.GetComponent<PlayerStats>().swordEquipped = true;
        player.GetComponent<PlayerStats>().sword = swordSprite.name;

        UpdateQuantity(-1);

        player.GetComponent<PlayerMovement>().swordEquipped = true;
        player.GetComponent<Equipped>().equipped = swordSprite.name;

        // trigger for how to attack tutorial
        if (!player.GetComponent<Equipped>().attackTutorial)
        {
            GameObject.Find("Attack Dialogue").GetComponent<DialogueTrigger>().TriggerDialogue();
            player.GetComponent<Equipped>().attackTutorial = true;
        }
    }

    public void Drop()
    {
        GetComponent<Spawn>().SpawnDroppedItem(name, itemType, sprite);
        if(name == "Copper Bar"){
            ChallengeMenu challengeMenu = GameObject.FindGameObjectWithTag("Challenges").GetComponent<ChallengeMenu>();
            challengeMenu.incrementChallenge("10cop");
        }
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
