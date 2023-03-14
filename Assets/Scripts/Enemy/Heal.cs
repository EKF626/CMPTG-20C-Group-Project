using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private float MinInterval;
    [SerializeField] private float MaxInterval;
    [SerializeField] private float HealAmount;

    private float _timer;
    private Enemy _enemy;

    private void Start() {
        _timer = Random.Range(MinInterval, MaxInterval);
        _enemy = transform.GetComponent<Enemy>();
    }

    private void Update() {
        _timer -= Time.deltaTime;
        if (_timer <= 0) {
            _timer = Random.Range(MinInterval, MaxInterval);
            _enemy.Heal(HealAmount);
        }
    }
}
