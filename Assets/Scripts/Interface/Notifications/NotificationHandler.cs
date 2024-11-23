using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Text;
    [SerializeField] private RectTransform m_Transform;

    public bool calculateSpeed = false;
    float speed;

    public int TextLenght => m_Text ? m_Text.text.Length : 0;
    public float TransformWidth => m_Transform ? m_Transform.sizeDelta.x : 0.0f;
    public void SetText(string text){
        m_Text.text = text;
        calculateSpeed = false;
        m_Transform.anchoredPosition = new Vector2(TransformWidth * 0.5f, 0.0f);
    }
    public void MoveTo(float time, Vector2 finalPos) {
        m_Transform.anchoredPosition = Vector2.Lerp(m_Transform.anchoredPosition, finalPos, time);
    }
}
