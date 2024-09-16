using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrapSystem_Scripts
{
    public class TrapSystemManager : MonoBehaviour
    {
        [SerializeField] private GameObject trapMenuUI;
        void Start()
        {
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Home))
            {
                ToggleTrapMenu();
            }
        }

        private void ToggleTrapMenu()
        {
            trapMenuUI.SetActive(!trapMenuUI.activeSelf);
        }
        
    }
    
}
