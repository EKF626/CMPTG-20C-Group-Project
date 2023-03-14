using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float Speed;

    private int _damage;
    private float _damageMod = 1;
    private GameObject _target;

    private void Update() {
        if (_target == null) {
            UnityEngine.Object.Destroy(this.gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Speed*Time.deltaTime/20f);
        if ((transform.position - _target.transform.position).magnitude < 0.1f) {
            Hit();
        }
    }

    private void Hit() {
        Dodge dodge = _target.GetComponent<Dodge>();
        if (dodge) {
            float rand = Random.Range(0, 1);
            if (rand < dodge.GetDodgeChance()) {
                UnityEngine.Object.Destroy(this.gameObject);
                return;
            }
        }
        _target.GetComponent<Enemy>().DealDamage(_damage*_damageMod);
        UnityEngine.Object.Destroy(this.gameObject);
    }

    public void SetInfo(int damage, GameObject target, float mod) {
        _damage = damage;
        _target = target;
        _damageMod = mod;
    }
}
