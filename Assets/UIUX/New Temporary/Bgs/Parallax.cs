using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{
    public Image uiImage;               // A imagem de UI para rolagem infinita
    public float scrollSpeed = 100f;     // Velocidade de rolagem
    private RectTransform rectTransform; // RectTransform da imagem
    private float imageHeight;           // Altura da imagem

    void Start()
    {
        rectTransform = uiImage.GetComponent<RectTransform>();
        imageHeight = rectTransform.rect.height; // Obtém a altura da imagem
        // Posiciona a imagem inicial em uma posição adequada
        rectTransform.anchoredPosition = new Vector2(0, 0); 
    }

    void Update()
    {
        // Move a imagem para baixo
        rectTransform.anchoredPosition += Vector2.down * scrollSpeed * Time.deltaTime;

        // Reposiciona a imagem assim que a parte inferior sair da tela
        if (rectTransform.anchoredPosition.y <= -imageHeight)
        {
            // Reposiciona para o topo da tela
            rectTransform.anchoredPosition += new Vector2(0, imageHeight);
        }
    }
}
