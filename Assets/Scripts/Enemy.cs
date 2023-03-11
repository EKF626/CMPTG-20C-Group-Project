using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType {None, PacMan, Sonic, Mario, Pokemon}

    [SerializeField] private EnemyType Type;
    [SerializeField] private float BaseSpeed;
    private float CurrentSpeed;
    [SerializeField] private float MaxHealth;
    private float CurrentHealth;

    public Waypoint Waypoint {get; set;}

    private int _currentWaypointIndex = 1;
    private float _distanceTraveled;
    private SpriteRenderer _spriteRenderer;

    public static event Action<int> DropCoins;
    public static event Action ReachedEnd;

    public Vector3 CurrentPointPosition => Waypoint.GetWaypointPosition(_currentWaypointIndex);

    private void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        CurrentSpeed = BaseSpeed;
        CurrentHealth = MaxHealth;
    }

    private void Update() {
        Move();

        if ((transform.position - CurrentPointPosition).magnitude < 0.1f) {
            if (_currentWaypointIndex < Waypoint.Points.Length-1) {
                _currentWaypointIndex++;
            }
            else {
                ReachedEnd?.Invoke();
                transform.parent.GetComponent<Spawner>().DecreaseTypeCount(Type);
                UnityEngine.Object.Destroy(this.gameObject);
            }
        }
    }

    private void Move() {
        Vector3 oldPos = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, CurrentSpeed*Time.deltaTime/20f);
        _distanceTraveled += Vector3.Distance(oldPos, transform.position);
    }

    public void DealDamage(float damage) {
        CurrentHealth -= damage;
        if (CurrentHealth < 0) {
            int coins = (int) UnityEngine.Random.Range((BaseSpeed+MaxHealth)*0.5f, (BaseSpeed+MaxHealth)*0.75f);
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
}
