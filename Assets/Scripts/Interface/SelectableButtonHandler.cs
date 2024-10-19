using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectableButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Color32 colorSelected, colorDeselected;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Select();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        animator.Play("Normal");
        text.color = colorDeselected;
    }

    public void Select()
    {
        animator.Play("Highlighted");
        text.color = colorSelected;
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }
}
