using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    [SerializeField] private float DodgeChance;

    public float GetDodgeChance() {
        return DodgeChance;
    }
}
