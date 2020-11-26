using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        GameObject.Find("UILayer").transform.Find("DialogueBox").gameObject.SetActive(true);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
