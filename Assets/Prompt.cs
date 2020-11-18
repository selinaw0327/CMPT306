using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    public void No()
    {
        GameObject.Find("Exit").GetComponent<Exit>().prompt = false;
        Destroy(GameObject.Find("Prompt"));
    }

    public void Yes()
    {
        GameObject.Find("Exit").GetComponent<Exit>().NextLevel();
        Destroy(GameObject.Find("Prompt"));
    }
}
