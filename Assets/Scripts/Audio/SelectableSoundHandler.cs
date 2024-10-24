using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class SelectableSoundHandler : MonoBehaviour, IPointerEnterHandler, /*IPointerExitHandler,*/ IPointerClickHandler, ISubmitHandler, ISelectHandler, IDeselectHandler
{
    [Header("Witch Sounds Will Play")]
    [SerializeField] bool playHoverSound;
    [SerializeField] bool playUnhoverSound;
    [SerializeField] bool playClickSound;

    [Header("Sounds To Play")]
    [SerializeField] UiSoundsList click;
    [SerializeField] UiSoundsList hover;
    


    public void OnPointerEnter(PointerEventData eventData){
        if (playHoverSound && PlayerInputHandler.currentControl == PlayerInputHandler.CurrentControl.Mouse)
            AudioManager.audioManager?.PlayUiSoundEffect(hover);
    }
    /*public void OnPointerExit(PointerEventData eventData){
        if (playUnhoverSound)
            AudioManager.audioManager?.PlayUiSoundEffect();
    }*/
    
     public void OnPointerClick(PointerEventData eventData){
        if (eventData.button == PointerEventData.InputButton.Left && playClickSound && PlayerInputHandler.currentControl == PlayerInputHandler.CurrentControl.Mouse)
             AudioManager.audioManager?.PlayUiSoundEffect(click);
     }

    public void OnSubmit(BaseEventData baseEventData){
        if (playClickSound && (PlayerInputHandler.currentControl == PlayerInputHandler.CurrentControl.Gamepad || PlayerInputHandler.currentControl == PlayerInputHandler.CurrentControl.Keyboard))
            AudioManager.audioManager?.PlayUiSoundEffect(click);
    }
    public void OnSelect(BaseEventData baseEventData){
        if (playHoverSound && (PlayerInputHandler.currentControl == PlayerInputHandler.CurrentControl.Gamepad || PlayerInputHandler.currentControl == PlayerInputHandler.CurrentControl.Keyboard))
            AudioManager.audioManager?.PlayUiSoundEffect(hover);
    }
    public void OnDeselect(BaseEventData baseEventData){// Only on mouse for now
        //if (playUnhoverSound)
        //    AudioManager.audioManager?.PlayUiSoundEffect(unhoverSound);
    }
}
