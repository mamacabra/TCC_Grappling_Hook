using Character;
using Character.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Character.Melee
{
    public class MeleeAttack : ACharacterMonoBehaviour
    {
        
        public GameObject meleeHitbox;
        private float timer = 1f;
        private bool isMeleeing;
       
         void Update()
        {
            if(isMeleeing)
            {
            timer -= Time.deltaTime;
            }
            
            if(timer<0f)
            {
                DeactivateHitbox();
            }
            
        }
        public void ActivateHitbox()
        {
            meleeHitbox?.SetActive(true);
            isMeleeing = true;
        }
        private void DeactivateHitbox()
        {
            meleeHitbox?.SetActive(false);
            isMeleeing = false;
            timer = 1f;
        }
    }

}
