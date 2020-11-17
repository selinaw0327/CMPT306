using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextLoader : MonoBehaviour
{

    public GameObject Text;

    private int i;

    private string[] dialogue = new string[] {
        "You awake to a sharp pain on the back of your head.", 
        "You look around and realize that you're laying down on cold earth, with cave walls trapping you in.", 
        "As you try to get up, your memories start to reveal themselves again, their momentary absence was a result of your fall by...", 
        "yes... that's right...", 
        "that crazy man who pushed you down in this underground cave.", 
        "You recall how he spoke of his desire to make a real-life survival game and how you were unfortunately chosen as the main character.", 
        "You get up and dust yourself off and you are met by a Ghost..."
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
        if (i < dialogue.Length)
        {
            Text.GetComponent<Text>().text = dialogue[i];
            i++;
        } else
        {
            SceneManager.LoadScene("TutorialScene");
        }
    }
    public void Prev()
    {
        if (i >= 2)
        {
            Text.GetComponent<Text>().text = dialogue[i-2];
            i -= 1;
        }
    }
}
