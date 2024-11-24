using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private NotificationHandler notificationHandler;
    [SerializeField] float maxT = 0.0f;
    [SerializeField] float notificationT = 0.0f;
    [SerializeField] float showT = 0.0f;
    [SerializeField] float hideT = 0.0f;


    bool show;
    bool hide;

    bool playing = false;
    const float ShowHideTime = 0.5f;

    public void PlayNotification(string text){
        notificationHandler.gameObject.SetActive(true);
        notificationHandler.SetText(text);
        maxT = notificationHandler.TextLenght * 0.05f;
        playing = true;
        show = true;
        hide = false;
        notificationT = 0.0f;
        showT = 0.0f;
        hideT = 0.0f;
    }

    private void Update() {
        if (playing) {
            float dt = Time.unscaledDeltaTime;
            if (show) {
                showT += dt;
                float time = Mathf.Clamp01(showT / ShowHideTime);
                var finalPos = new Vector2(notificationHandler.TransformWidth * -0.5f, 0.0f);
                notificationHandler.MoveTo(time, finalPos);
                if (showT >= ShowHideTime) {
                    notificationT = maxT;
                    show = false;
                }
            }
            if (notificationT > 0.0f) {
                notificationT -= dt;
                if (notificationT <= 0.0f) { hide = true; }
            }
            if (hide) {
                hideT += dt;
                float time = Mathf.Clamp01(hideT / ShowHideTime);
                var finalPos = new Vector2(notificationHandler.TransformWidth * 0.5f, 0.0f);
                notificationHandler.MoveTo(time, finalPos);
                if (hideT >= ShowHideTime) {
                    hide = false;
                    playing = false;
                    notificationHandler.gameObject.SetActive(false);
                }
            }
        }
    }
}
