using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScreen : Screens
{
   [Header("Graphics")] 
   [Header("Resolution")]
   [SerializeField] TextMeshProUGUI resolutionValue_Text;
   [SerializeField] private List<Button> leftandRigth_Resolution_Buttons;
   
   [Header("Ecra")] 
   [SerializeField] TextMeshProUGUI ecraValue_Text;
   [SerializeField] private List<Button> leftandRigth_Ecra_Buttons;
   
   [Header("Quality")] 
   [SerializeField] TextMeshProUGUI qualityValue_Text;
   [SerializeField] private List<Button> leftandRigth_Quality_Buttons;
   
   [Header("Audio")] 
   [Header("SFX")] 
   [SerializeField] TextMeshProUGUI sFXValue_Text;
   [SerializeField] private List<Button> leftandRigth_SFX_Buttons;
   
   [Header("Sound")] 
   [SerializeField] TextMeshProUGUI soundValue_Text;
   [SerializeField] private List<Button> leftandRigth_Sound_Buttons;

   [Header("Back Button")] [SerializeField]
   private ButtonToScreen backButton;
   
   [Header("Restore button")] [SerializeField]
   private Button restoreButton;
}
