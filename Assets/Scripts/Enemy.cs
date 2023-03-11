using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float BaseSpeed;
    private float CurrentSpeed;
    [SerializeField] private float MaxHealth;
    private float CurrentHealth;

    public Waypoint Waypoint {get; set;}

    private int _currentWaypointIndex = 1;
    private int _posInWave;
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
                UnityEngine.Object.Destroy(this.gameObject);
            }
        }
    }

    private void Move() {
        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, CurrentSpeed*Time.deltaTime/20f);
    }

    public void DealDamage(float damage) {
        CurrentHealth -= damage;
        if (CurrentHealth < 0) {
            UnityEngine.Object.Destroy(this.gameObject);
            int coins = (int) UnityEngine.Random.Range((BaseSpeed+MaxHealth)*0.5f, (BaseSpeed+MaxHealth)*0.75f);
            DropCoins?.Invoke(coins);
        }
    }

    public void AssignPosInWave(int position) {
        _posInWave = position;
    }

    public int GetPosInWave() {
        return _posInWave;
    }
}
