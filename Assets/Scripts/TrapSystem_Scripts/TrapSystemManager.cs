using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace TrapSystem_Scripts
{
    public class TrapSystemManager : MonoBehaviour
    {
        [SerializeField] private GameObject trapMenuUI;
        [SerializeField] private float timeToActivateTrap = 5.0f;
        [SerializeField] private GameObject shadowTrap;
        public Toggle shadowTrapToggle;

        private void Start()
        { 
            StartCoroutine(ActivateTrapAfterTime());
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

        private IEnumerator ActivateTrapAfterTime()
        {
            yield return new WaitForSeconds(timeToActivateTrap); 
            if (shadowTrapToggle.isOn)
            {
                shadowTrap.SetActive(true);
            }
        }
        
    }
    
}
