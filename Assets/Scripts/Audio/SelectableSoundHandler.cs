using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class SelectableSoundHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, ISubmitHandler, ISelectHandler, IDeselectHandler
{
    [Header("Witch Sounds Will Play")]
    [SerializeField] bool playHoverSound;
    [SerializeField] bool playUnhoverSound;
    [SerializeField] bool playClickSound;

    [Header("Sounds To Play")]
    [SerializeField] string hoverSound = "Select";
    [SerializeField] string unhoverSound = "Cancel";
    [SerializeField] string clickSound = "Confirm";

    public void OnPointerEnter(PointerEventData eventData){
        if (playHoverSound)
            AudioManager.audioManager?.PlayUiSoundEffect(hoverSound);
    }
    public void OnPointerExit(PointerEventData eventData){
        if (playUnhoverSound)
            AudioManager.audioManager?.PlayUiSoundEffect(unhoverSound);
    }
    public void OnPointerClick(PointerEventData eventData){
        if (eventData.button == PointerEventData.InputButton.Left && playClickSound)
            AudioManager.audioManager?.PlayUiSoundEffect(clickSound);
    }
    public void OnSubmit(BaseEventData baseEventData){// Only on mouse for now
        //if (playClickSound)
        //    AudioManager.audioManager?.PlayUiSoundEffect(clickSound);
    }
    public void OnSelect(BaseEventData baseEventData){// Only on mouse for now
        //if (playHoverSound)
        //    AudioManager.audioManager?.PlayUiSoundEffect(hoverSound);
    }
    public void OnDeselect(BaseEventData baseEventData){// Only on mouse for now
        //if (playUnhoverSound)
        //    AudioManager.audioManager?.PlayUiSoundEffect(unhoverSound);
    }
}
