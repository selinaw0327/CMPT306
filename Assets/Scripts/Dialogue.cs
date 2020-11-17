using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    //public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public Animator textDisplayAnim;
    private AudioSource source;

    public readonly string[] sentences = new string[] {
        "GHOST: I was like you once, thrown down in this cave. I wasn’t able to escape but I can help you get started. I wish you a better fate than the one I ended up with.",
        "GHOST: Use WASD or the Arrow Keys on your keyboard to move around the map. Careful! The creatures down here are not friendly. If you get too close to them, they will start to attack you.",
        "GHOST: Pressing [ I ] on your keyboard will show and hide your inventory.",
        "GHOST: To pick up items from the ground, simply walk by the item. That item will then be added to your inventory. If you ever want to drop an item, left-click on that item’s inventory slot. To use an item, right-click on it instead.",
        "GHOST: You can also press the number keys on your keyboard that match the inventory slot to use that item.",
        "GHOST: You will need to find food to sustain your health. If your health drops to zero by either starving or being attacked, you’ll die and end up like me. Stuck here forever.",
        "GHOST: You need to fend for yourself, fight off foes, forge your way out of this cave and survive. I wish you luck."
    };

    public GameObject textBox;

    void Start()
    {
        textBox.SetActive(true);
        source = GetComponent<AudioSource>();
        StartCoroutine(Type());
    }

    void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void NextSentence()
    {
        source.Play();

        textDisplayAnim.SetTrigger("Change");

        continueButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        } else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
            textBox.SetActive(false);
        }
    }
}
