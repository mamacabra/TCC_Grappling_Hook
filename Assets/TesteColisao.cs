using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteColisao : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            Debug.Log("COLISAO");
        }
    }
}
