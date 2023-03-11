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

        if (_enemiesInRange.Count > 0) {
            _currentEnemy = _enemiesInRange[0];
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

    private void OnTriggerEnter2D(Collider2D col) {
        int newEnemyPosition = col.gameObject.GetComponent<Enemy>().GetPosInWave();
        int i;
        for (i = 0; i < _enemiesInRange.Count; i++) {
            if (_enemiesInRange[i].GetComponent<Enemy>().GetPosInWave() > newEnemyPosition) {
                break;
            }
        }
        _enemiesInRange.Insert(i, col.gameObject);
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
