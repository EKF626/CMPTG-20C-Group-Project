using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType {None, PacMan, Sonic, Mario, Pokemon}

    [SerializeField] private EnemyType Type;
    [SerializeField] private float BaseSpeed;
    [SerializeField] private float MaxHealth;

    public Waypoint Waypoint {get; set;}

    private int _currentWaypointIndex = 1;
    private float _speedMod = 1;
    private float _currentHealth;
    private float _distanceTraveled;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    public static event Action<int> DropCoins;
    public static event Action ReachedEnd;

    public Vector3 CurrentPointPosition => Waypoint.GetWaypointPosition(_currentWaypointIndex);

    private void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        _currentHealth = MaxHealth;
    }

    private void Update() {
        Move();
        if ((transform.position - CurrentPointPosition).magnitude < 0.1f) {
            NextWaypoint();
        }
    }

    private void Move() {
        Vector3 oldPos = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, BaseSpeed*_speedMod*Time.deltaTime/20f);
        _distanceTraveled += Vector3.Distance(oldPos, transform.position);
    }

    private void NextWaypoint() {
        if (_currentWaypointIndex < Waypoint.Points.Length-1) {
            _currentWaypointIndex++;
            if (_animator) {
                if (CurrentPointPosition.x > transform.position.x) {
                    _animator.SetTrigger("Right");
                } else {
                    _animator.SetTrigger("Left");
                }
            }
        }
        else {
            ReachedEnd?.Invoke();
            transform.parent.GetComponent<Spawner>().DecreaseTypeCount(Type);
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }

    public void DealDamage(float damage) {
        _currentHealth -= damage;
        if (_currentHealth < 0) {
            int coins = (int) UnityEngine.Random.Range((BaseSpeed+MaxHealth)*0.12f, (BaseSpeed+MaxHealth)*0.7f);
            DropCoins?.Invoke(coins);
            transform.parent.GetComponent<Spawner>().DecreaseTypeCount(Type);
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }

    public float GetDistanceTraveled() {
        return _distanceTraveled;
    }

    public EnemyType GetEnemyType() {
        return Type;
    }

    public void MultSpeedMod(float mod) {
        _speedMod *= mod;
    }

    public void Heal(float amount) {
        _currentHealth += MaxHealth*amount;
        if (_currentHealth > MaxHealth) {
            _currentHealth = MaxHealth;
        }
    }
}
