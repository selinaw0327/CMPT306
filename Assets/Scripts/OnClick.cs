using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GetComponent<UseDrop>().Drop();
        }            
        //else if (eventData.button == PointerEventData.InputButton.Middle)
        //{
        //    Debug.Log("Middle click");
        //}            
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            GetComponent<UseDrop>().Use();
        }
            
    }

}
