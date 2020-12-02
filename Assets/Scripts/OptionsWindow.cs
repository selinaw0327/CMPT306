using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsWindow : MonoBehaviour
{
    public GameObject options;

    public void openOptions()
    {
        if (options != null)
        {
            options.SetActive(true);
        }
    }

    public void closeOptions()
    {
        if (options != null)
        {
            options.SetActive(false);
        }
    }
}
