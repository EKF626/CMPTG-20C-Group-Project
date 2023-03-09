using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData 
{
    public int NumEnemies;
    public float MinSpawnTime;
    public float MaxSpawnTime;
    public GameObject[] EnemyPrefabs;
    public float[] Probabilities;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] float InitialDelay;

    public WaveData[] WaveData;

    private Waypoint _waypoint;
    private float _spawnTimer = 0;
    private int _enemiesSpawned = 0;
    private int _currentWave = 0;
    private bool _waitingForNextWave = true;

    public static event Action WaveOver;

    private void Start() {
        _waypoint = GetComponent<Waypoint>();
    }

    private void Update() {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0f && !_waitingForNextWave) {
            if (_enemiesSpawned < WaveData[_currentWave].NumEnemies) {
                _spawnTimer = UnityEngine.Random.Range(WaveData[_currentWave].MinSpawnTime, WaveData[_currentWave].MaxSpawnTime);
                _enemiesSpawned++;
                SpawnEnemy();
            }
            else if (transform.childCount == 0) {
                if (_currentWave < WaveData.Length - 1) {
                    _currentWave++;
                    _waitingForNextWave = true;
                    WaveOver?.Invoke();
                }
                else {
                    //That's the end.
                }
            }
        }
    }

    private void SpawnEnemy() {
        GameObject newEnemy = Instantiate(GetEnemyChoice());
        newEnemy.transform.SetParent(transform);
        newEnemy.transform.position = _waypoint.GetWaypointPosition(0);
        newEnemy.GetComponent<Enemy>().Waypoint = _waypoint;
        newEnemy.SetActive(true);
    }

    private GameObject GetEnemyChoice() {
        float randVal = UnityEngine.Random.Range(0f, 1f);
        float totalChance = 0f;
        for (int i = 0; i < WaveData[_currentWave].EnemyPrefabs.Length; i++) {
            totalChance += WaveData[_currentWave].Probabilities[i];
            if (randVal < totalChance) {
                return WaveData[_currentWave].EnemyPrefabs[i];
            }
        }
        return null;
    }

    public int GetWaveNumber() {
        return _currentWave;
    }

    public void StartWave() {
        _enemiesSpawned = 0;
        _waitingForNextWave = false;
    }
}
