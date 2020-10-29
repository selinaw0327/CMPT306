﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject[] objects;

    // Start is called before the first frame update
    private void Start()
    {
        int rand = Random.Range(0, objects.Length);
        GameObject instance = (GameObject)Instantiate(objects[rand],transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }
}
