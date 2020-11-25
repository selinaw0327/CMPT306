using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipTutorialHandler : MonoBehaviour
{
    public void disableDialogue(){

        // disable DialogueSystem gameobject
    
        GameObject dialogue = GameObject.Find("DialogueBox");

        dialogue.SetActive(false);
    }
}
