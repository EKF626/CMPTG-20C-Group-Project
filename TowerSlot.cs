using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlot : MonoBehaviour
{
    private bool _taken = false;

    public bool IsTaken() {
        return _taken;
    }

    public void SetTaken(bool set) {
        _taken = set;
    }
}
