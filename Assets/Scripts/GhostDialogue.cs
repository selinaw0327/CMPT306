using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDialogue : MonoBehaviour
{    private void OnTriggerEnter2D(Collider2D entity)
    {
        if (entity.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Ghost Dialogue").GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }
}
