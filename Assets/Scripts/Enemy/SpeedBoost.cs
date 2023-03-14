using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private float MinInterval;
    [SerializeField] private float MaxInterval;
    [SerializeField] private float BoostLength;
    [SerializeField] private float BoostMod;

    private float _timer;
    private bool _boosting = false;
    private Enemy _enemy;

    private void Start() {
        _timer = Random.Range(MinInterval, MaxInterval);
        _enemy = transform.GetComponent<Enemy>();
    }

    private void Update() {
        _timer -= Time.deltaTime;
        if (_timer <= 0) {
            if (_boosting) {
                _enemy.MultSpeedMod(1/BoostMod);
                _timer = Random.Range(MinInterval, MaxInterval);
            }
            else {
                _enemy.MultSpeedMod(BoostMod);
                _timer = BoostLength;
            }
            _boosting = !_boosting;
        }
    }
}
