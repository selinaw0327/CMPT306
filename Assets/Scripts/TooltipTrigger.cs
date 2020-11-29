using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string content;
    public string header;

    private void Start()
    {
        if (string.IsNullOrEmpty(content) && string.IsNullOrEmpty(header))
        {
            TooltipSystem.Hide();
        }
    }

    private void Update()
    {
        content = gameObject.GetComponent<Slot>().GetContent();
        header = gameObject.GetComponent<Slot>().GetHeader();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(header))
        {
            content = gameObject.GetComponent<Slot>().GetContent();
            header = gameObject.GetComponent<Slot>().GetHeader();
            TooltipSystem.Show(header, content);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }

}
