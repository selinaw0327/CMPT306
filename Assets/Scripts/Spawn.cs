using System.Collections;
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

    public void SpawnDroppedItem(Sprite sprite)
    {
        Vector2 playerPos = new Vector2(player.position.x, player.position.y + 2);
        GameObject newItem = Instantiate(item, playerPos, Quaternion.identity);

        newItem.GetComponent<SpriteRenderer>().sprite = sprite;

        newItem.AddComponent<CircleCollider2D>();
        newItem.GetComponent<CircleCollider2D>().isTrigger = true;
        newItem.GetComponent<CircleCollider2D>().radius = 0.25f;
    }
}
