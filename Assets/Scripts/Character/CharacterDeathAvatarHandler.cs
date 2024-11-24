using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CharacterDeathAvatarHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody[] rigidbodies;
    private const float Force = 5f;

    public void AddForceToBodies(Vector3 dir){
        for (int i = 0; i < rigidbodies.Length; i++) {
            rigidbodies[i].AddForce(dir.normalized * Force, ForceMode.Impulse);
        }
    }
}
