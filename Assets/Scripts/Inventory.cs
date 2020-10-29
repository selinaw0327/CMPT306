using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public bool[] isFull;
    public GameObject[] slots;

    public GameObject inventory;

    private bool show = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int IndexOf(GameObject slot)
    {
        return System.Array.IndexOf(slots, slot);
    }
}
