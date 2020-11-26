using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClick : MonoBehaviour, IPointerClickHandler
{
    public bool quantityObject;
    public int index;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if(quantityObject)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().inventoryItems[index].GetComponent<UseDrop>().Drop();
            } else
            {
                GetComponent<UseDrop>().Drop();
            }
            
        }            
        //else if (eventData.button == PointerEventData.InputButton.Middle)
        //{
        //    Debug.Log("Middle click");
        //}            
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (quantityObject)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().inventoryItems[index].GetComponent<UseDrop>().Use();
            }
            else
            {
                GetComponent<UseDrop>().Use();
            }
        }
            
    }

}
