using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public LayoutElement layoutElement;

    public int characterWrapLimit;

    public RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string header, string content = "")
    {
        if (string.IsNullOrEmpty(content))
        {
            contentField.gameObject.SetActive(false);
        }
        else
        {
            contentField.gameObject.SetActive(true);
            contentField.text = content;
        }

        headerField.text = header;

        int headerLength = header.Length;
        int contentLength = content.Length;

        layoutElement.enabled = (contentLength > characterWrapLimit || headerLength > characterWrapLimit) ? true : false;

    }

    private void Update()
    {
        Vector2 position = Input.mousePosition;

        transform.position = position;
    }

}
