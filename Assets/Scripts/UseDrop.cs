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

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        slot = transform.parent.gameObject;
        sprite = gameObject.GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Drop()
    {
        GetComponent<Spawn>().SpawnDroppedItem(sprite);
        Destroy(gameObject);
        inventory.itemDataArr[inventory.IndexOf(slot)] = null;
        inventory.occupied[inventory.IndexOf(slot)] = false;

    }
}
