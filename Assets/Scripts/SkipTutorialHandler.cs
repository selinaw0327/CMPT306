using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipTutorialHandler : MonoBehaviour
{
    public void disableDialogue(){

        // disable DialogueSystem gameobject
        GameObject.Find("UILayer").transform.Find("DialogueBox").gameObject.SetActive(false);
    }
}
