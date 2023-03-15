using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int StartingCoins;
    [SerializeField] private int StartingLives;

    private int _coins;
    private int _lives;

    public static event Action Lose;

    private void Start() {
        _coins = StartingCoins;
        _lives = StartingLives;
    }

    public int GetCoins() {
        return _coins;
    }

    public int GetLives() {
        return _lives;
    }

    public void ChangeCoins(int amount) {
        _coins += amount;
    }

    public void TakeDamage() {
        _lives -= 1;
        if (_lives <= 0) {
            Lose?.Invoke();
        }
    }

    private void OnEnable() {
        Enemy.DropCoins += ChangeCoins;
        Enemy.ReachedEnd += TakeDamage;
    }

    private void OnDisable() {
        Enemy.DropCoins -= ChangeCoins;
        Enemy.ReachedEnd -= TakeDamage;
    }
}
