using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextLoader : MonoBehaviour
{

    public GameObject Text;

    public bool introScene;

    private int i;

    private readonly string[] intro = new string[] {
        "You awake to a sharp pain on the back of your head.", 
        "You look around and realize that you're laying down on cold earth, with cave walls trapping you in.", 
        "As you try to get up, your memories start to reveal themselves... their momentary absence was a result of your fall by...", 
        "yes... that's right...", 
        "that crazy man who pushed you down in this underground cave.", 
        "You recall how he spoke of his desire to make a real-life survival game and how you were unfortunately chosen as the main character.", 
        "You get up and dust yourself off and you are met by a Ghost..."
    };

    private readonly string[] outro = new string[] {
        "Exhausted, famished and weak you trudged down the final cave tunnel.",
        "Your hand ran along the cave walls, it's familiar ridges and rocks providing you some support to stay upright...",
        "While your other hand clutches the Obsidian sword that aided you in succeeding against the villain.",
        "\"He can't hurt anyone anymore,\" you think to yourself.",
        "It was a tough battle, one that you wouldn't wish on anyone. but at least now, thanks to you...",
        "No one will ever have to go through that again."
    };


    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        Next();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        if (introScene)
        {
            if (i < intro.Length)
            {
                Text.GetComponent<Text>().text = intro[i];
                i++;
            }
            else
            {
                SceneManager.LoadScene("TutorialScene");
            }
        } else
        {
            if (i < outro.Length)
            {
                Text.GetComponent<Text>().text = outro[i];
                i++;
            }
            else
            {
                SceneManager.LoadScene("TutorialScene");
            }
        }
    }
    public void Prev()
    {
        if (introScene)
        {
            if (i >= 2)
            {
                Text.GetComponent<Text>().text = intro[i - 2];
                i -= 1;
            }
        } else
        {
            if (i >= 2)
            {
                Text.GetComponent<Text>().text = outro[i - 2];
                i -= 1;
            }
        }
    }
}
