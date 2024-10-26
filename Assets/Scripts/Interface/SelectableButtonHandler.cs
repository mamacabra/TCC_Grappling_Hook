using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectableButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Select();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //animator.Play("Normal");
        //text.color = colorDeselected;
    }

    public void Select()
    {
        //animator.Play("Highlighted");
        //text.color = colorSelected;
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }
    
    
}
