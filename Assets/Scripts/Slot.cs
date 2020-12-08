using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public GameObject quantity;

    public string itemName;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void UpdateQuantity(int i)
    {
        if (i == 0)
        {
            quantity.GetComponent<Text>().text = "";
            itemName = "";
        }
        else
        {
            quantity.GetComponent<Text>().text = i.ToString();
        }
    }

    public string Name()
    {
        return itemName;
    }

    public string GetHeader()
    {
        string header;

        switch (itemName)
        {
            case "Apple":
                header = "Apple";
                break;
            case "Bread":
                header = "Bread";
                break;
            case "Steak":
                header = "Steak";
                break;
            case "Copper Bar":
                header = "Copper Bar";
                break;
            case "Iron Bar":
                header = "Iron Bar";
                break;
            case "Gold Bar":
                header = "Gold Bar";
                break;
            case "Obsidian Bar":
                header = "Obsidian Bar";
                break;
            case "Copper Sword":
                header = "Copper Sword";
                break;
            case "Iron Sword":
                header = "Iron Sword";
                break;
            case "Gold Sword":
                header = "Gold Sword";
                break;
            case "CopperArmour":
                header = "Copper Armour";
                break;
            case "ObsidianArmour":
                header = "Obsidian Armour";
                break;
            default:
                header = "";
                break;
        }

        return header;

    }
    public string GetContent()
    {
        string content;

        switch (itemName)
        {
            case "Apple":
                content = "Heals for 10 health.";
                break;
            case "Bread":
                content = "Heals for 30 health.";
                break;
            case "Steak":
                content = "Heals for 50 health.";
                break;
            case "Copper Bar":
                content = "Collect 10 Copper Bars to forge a Copper Sword";
                break;
            case "Iron Bar":
                content = "Collect 10 Iron Bars to forge an Iron Sword";
                break;
            case "Gold Bar":
                content = "Collect 10 Gold Bars to forge a Gold Sword";
                break;
            case "Obsidian Bar":
                content = "Collect 10 Obsidian Bars to forge an Obsidian Sword";
                break;
            case "Copper Sword":
                content = "";
                break;
            case "Iron Sword":
                content = "Increases damage dealt by 25.";
                break;
            case "Gold Sword":
                content = "Increases damage dealt by 50.";
                break;
            case "Obsidian Sword":
                content = "Increases damage dealt by 100.";
                break;
            case "CopperArmour":
                content = "Increases base max health by 100";
                break;
            case "ObsidianArmour":
                content = "Increases base max health by 200";
                break;
            default:
                content = "";
                break;
        }

        return content;

    }


}
