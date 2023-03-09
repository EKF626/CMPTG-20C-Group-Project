using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float Damage;
    [SerializeField] private float FireSpeed;
    [SerializeField] private GameObject Projectile;

    private float _fireTimer = 0f;
    private Queue<GameObject> _enemiesInRange = new Queue<GameObject>();
    private TowerSlot _slot;

    private void Update() {
        _fireTimer -= Time.deltaTime;

        while (_enemiesInRange.Count > 0 && _enemiesInRange.Peek() == null) {
            _enemiesInRange.Dequeue();
        }

        if (_enemiesInRange.Count > 0) {
            Rotate();
            if (_fireTimer <= 0) {
                Fire();
                _fireTimer = FireSpeed;
            }
        }
    }

    private void Rotate() {
        GameObject currentEnemy = _enemiesInRange.Peek();
        float angle = Mathf.Atan2(currentEnemy.transform.position.y - transform.position.y, currentEnemy.transform.position.x -transform.position.x ) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 150 * Time.deltaTime);
    }

    private void Fire() {
        GameObject newProjectile = Instantiate(Projectile);
        newProjectile.transform.position = transform.position;
        newProjectile.GetComponent<Projectile>().SetTarget(_enemiesInRange.Peek());
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.GetComponent<Enemy>() != null) {
            _enemiesInRange.Enqueue(col.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject == _enemiesInRange.Peek()) {
            _enemiesInRange.Dequeue();
        }
    }

    public void SetTowerSlot(TowerSlot slot) {
        _slot = slot;
    }
}
