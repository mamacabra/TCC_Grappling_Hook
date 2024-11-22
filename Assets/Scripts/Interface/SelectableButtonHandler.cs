using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableButtonHandler : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Select();
    }
    public void Select()
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }


    [Header("Color Change")]
    [SerializeField] private Image outlineImage;

    [SerializeField] private TextMeshProUGUI buttonText;

    [SerializeField] private Color32 buttonTextNormalAndDisableColor = new Color32(255,255,255,255);
    [SerializeField] private Color32 buttonTextHighAndSelectedColor = new Color32(255,255,255,255);
    
    public void NormalAndDisable()
    {
        outlineImage.color = new Color(255, 255, 255, 0);
        buttonText.color = buttonTextNormalAndDisableColor;

        if (hasToChangeTextImage)  {
            if(image2Img1) image2Img1.color = buttonTextNormalAndDisableColor;
            if(image2Img2) image2Img2.color = buttonTextNormalAndDisableColor;
            if(image2Img3) image2Img3.color = buttonTextNormalAndDisableColor;
            if(text2_1) text2_1.color = buttonTextNormalAndDisableColor;
            if(text2_2) text2_2.color = buttonTextNormalAndDisableColor;
            if(text2_3) text2_3.color = buttonTextNormalAndDisableColor;
        }
    }
    
    public void HighAndSelected()
    {
        Select();
        
        outlineImage.color = buttonTextHighAndSelectedColor;
        buttonText.color = buttonTextHighAndSelectedColor;

        if (hasToChangeTextImage)
        {
            if(image2Img1) image2Img1.color = buttonTextHighAndSelectedColor;
            if(image2Img2) image2Img2.color = buttonTextHighAndSelectedColor;
            if(image2Img3) image2Img3.color = buttonTextHighAndSelectedColor;
            if(text2_1) text2_1.color = buttonTextHighAndSelectedColor;
            if(text2_2) text2_2.color = buttonTextHighAndSelectedColor;
            if(text2_3) text2_3.color = buttonTextHighAndSelectedColor;
        }
    }

    [Header("Anim")] 
    [SerializeField] private bool hasToChangeText = false;

    [SerializeField] private TextMeshProUGUI textToChange;
    [SerializeField] private string text1, text2;
    
    [SerializeField] private bool hasToChangeTextImage = false;
    [SerializeField] private GameObject image1, image2;
    [SerializeField] private Image image2Img1, image2Img2, image2Img3;
    [SerializeField] private TextMeshProUGUI text2_1, text2_2,text2_3;

    Coroutine current;

    private bool click;
    private void OnEnable()
    {
        click = false;
        if(hasToChangeText) ChangeAnimText();
        if (hasToChangeTextImage) ChangeAnimImage();
    }

    public void OnClick()
    {
        click = true;
        if (current != null) StopCoroutine(current);
    }

    public void ChangeAnimText()
    {
        if (current != null) StopCoroutine(current);
        current = StartCoroutine(ChangeText());
        IEnumerator ChangeText()
        {
            textToChange.text = text1;
            yield return new WaitForSeconds(1f);
            textToChange.text = text2;
            yield return new WaitForSeconds(1f);
            if (current != null) StopCoroutine(current);
            if(click) yield break;
            current = StartCoroutine(ChangeText());
        }
    }

    public void ChangeAnimImage()
    {
        if (current != null) StopCoroutine(current);
        current = StartCoroutine(Change());
       
        IEnumerator Change()
        {
            image1.SetActive(true); image2.SetActive(false);
            yield return new WaitForSeconds(1f);
            image1.SetActive(false); image2.SetActive(true);
            yield return new WaitForSeconds(1f);
            if (current != null) StopCoroutine(current);
            
            if(click) yield break;
            current = StartCoroutine(Change());
        }
    }
}
