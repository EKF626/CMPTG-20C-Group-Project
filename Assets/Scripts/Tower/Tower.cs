using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private List<GameObject> _enemiesInRange = new List<GameObject>();
    private TowerSlot _slot;

    private void Update() {

    }

    public GameObject GetCurrentEnemy() {
        if (_enemiesInRange.Count == 0) {
            return null;
        }
        else {
            float maxDist = _enemiesInRange[0].GetComponent<Enemy>().GetDistanceTraveled();
            GameObject maxEnemy = _enemiesInRange[0];
            for (int i = 1; i < _enemiesInRange.Count; i++) {
                float dist = _enemiesInRange[i].GetComponent<Enemy>().GetDistanceTraveled();
                if (dist > maxDist) {
                    maxDist = dist;
                    maxEnemy = _enemiesInRange[i];
                }
            }
            return maxEnemy;
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.GetComponent<Enemy>() && !_enemiesInRange.Contains(col.gameObject)) {
            _enemiesInRange.Add(col.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (_enemiesInRange.Contains(col.gameObject)) {
            _enemiesInRange.Remove(col.gameObject);
        }
    }

    public void SetTowerSlot(TowerSlot slot) {
        _slot = slot;
    }

    public List<GameObject> GetEnemiesInRange() {
        return _enemiesInRange;
    }
}
