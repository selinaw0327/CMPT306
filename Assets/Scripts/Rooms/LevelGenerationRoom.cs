﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerationRoom : MonoBehaviour
{

    public Transform[] startingPositions;
    public GameObject[] rooms;

    private int direction;
    public float moveAmount;

    private float timeBtwRoom;
    public float startBtwRoom = 0.25f;

    public float minX;
    public float maxX;
    public float minY;
    private bool stopGeneration;

    // Start is called before the first frame update
    private void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);
    }

    private void Update()
    {
        if (timeBtwRoom <= 0 && stopGeneration == false )
        {
            Move();
            timeBtwRoom = startBtwRoom;
        } else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    private void Move()
    {
        if (direction == 1 || direction == 2) // Move Right !
        {
            if (transform.position.x < maxX)
            {
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;
            } else
            {
                direction = 5;
            }
        } else if (direction == 3 || direction == 4) // Move Left !
        {
            if (transform.position.x > minX)
            {
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;
            } else
            {
                direction = 5;
            } 
        } else if (direction == 5) // Move Down !
        {
            if (transform.position.y > minY)
            {
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;
            } else
            {
                // STOP LEVEL GENERATION !
                stopGeneration = true;
            }
        }

        Instantiate(rooms[0], transform.position, Quaternion.identity);
        direction = Random.Range(1, 6);
    }
}
