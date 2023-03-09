using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float Damage;
    [SerializeField] private float Speed;

    private GameObject _target;

    private void Update() {
        if (_target == null) {
            UnityEngine.Object.Destroy(this.gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Speed*Time.deltaTime/20f);
        if ((transform.position - _target.transform.position).magnitude < 0.1f) {
            _target.GetComponent<Enemy>().DealDamage(Damage);
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }

    public void SetTarget(GameObject target) {
        _target = target;
    }
}
