using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private int Damage;
    [SerializeField] private float FireSpeed;
    [SerializeField] private GameObject Projectile;

    private float _fireTimer = 0;
    private float _speedMod = 1;
    private float _damageMod = 1;
    private GameObject _currentEnemy;
    private Tower _tower;

    private void Start() {
        _tower = transform.GetComponent<Tower>();
    }

    private void Update() {
        _fireTimer -= Time.deltaTime*_speedMod;
        _currentEnemy = _tower.GetCurrentEnemy();
        if (_currentEnemy) {
            if (_fireTimer <= 0) {
                Fire();
                _fireTimer = FireSpeed;
            }
        }
    }

    private void Fire() {
        GameObject newProjectile = Instantiate(Projectile);
        newProjectile.transform.position = transform.position;
        newProjectile.GetComponent<Projectile>().SetInfo(Damage, _currentEnemy, _damageMod);
    }

    public void MultMods(float speed, float damage) {
        _speedMod *= speed;
        _damageMod *= damage;
    }
}
