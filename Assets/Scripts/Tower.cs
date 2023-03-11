using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float Damage;
    [SerializeField] private float FireSpeed;
    [SerializeField] private GameObject Projectile;

    private float _fireTimer = 0f;
    private List<GameObject> _enemiesInRange = new List<GameObject>();
    private GameObject _currentEnemy;
    private TowerSlot _slot;

    private void Update() {
        _fireTimer -= Time.deltaTime;

        _currentEnemy = GetCurrentEnemy();
        if (_currentEnemy) {
            Rotate();
            if (_fireTimer <= 0) {
                Fire();
                _fireTimer = FireSpeed;
            }
        }
    }

    private void Rotate() {
        float angle = Mathf.Atan2(_currentEnemy.transform.position.y - transform.position.y, _currentEnemy.transform.position.x -transform.position.x ) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 150 * Time.deltaTime);
    }

    private void Fire() {
        GameObject newProjectile = Instantiate(Projectile);
        newProjectile.transform.position = transform.position;
        newProjectile.GetComponent<Projectile>().SetTarget(_currentEnemy);
    }

    private GameObject GetCurrentEnemy() {
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
}
