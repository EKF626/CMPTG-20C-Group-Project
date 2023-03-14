using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlow : MonoBehaviour
{
    [SerializeField] private float SlowMod;

    private Tower _tower;

    private void Start() {
        _tower = transform.GetComponent<Tower>();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.GetComponent<Enemy>()) {
            col.gameObject.GetComponent<Enemy>().MultSpeedMod(SlowMod);
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.GetComponent<Enemy>()) {
            col.gameObject.GetComponent<Enemy>().MultSpeedMod(1/SlowMod);
        }
    }
}
