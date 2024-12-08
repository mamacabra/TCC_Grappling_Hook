using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    private Animator animator;
    private Image image;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
    }

    public void Disable()
    {
        animator.enabled = false;
        gameObject.SetActive(false);
        image.color = new Color32(240, 204, 169, 255);
        animator.enabled = true;
    }
}
