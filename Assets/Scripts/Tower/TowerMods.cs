using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMods : MonoBehaviour
{
    [SerializeField] private float SpeedMod;
    [SerializeField] private float DamageMod;

    private void OnTriggerEnter2D(Collider2D col) {
        Shooter shooter = col.gameObject.GetComponent<Shooter>();
        if (shooter) {
            shooter.MultMods(SpeedMod, DamageMod);
        }
    }
}
