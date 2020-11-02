using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public bool[] occupied;
    public GameObject[] slots;
    public InventoryItemData[] itemDataArr;

    public GameObject inventory;

    private bool show = true;
    private string[] items;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            inventory.SetActive(!show);
            ChallengeMenu challengeMenu = GameObject.FindGameObjectWithTag("Challenges").GetComponent<ChallengeMenu>();
            challengeMenu.updateChallenge("inv");
            show = !show;
        }
    }

    public int IndexOf(GameObject slot)
    {
        return System.Array.IndexOf(slots, slot);
    }
}
