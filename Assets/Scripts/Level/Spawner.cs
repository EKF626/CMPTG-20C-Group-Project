using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData 
{
    public int NumEnemies;
    public int MinSpawnTime;
    public int MaxSpawnTime;
    public GameObject[] EnemyPrefabs;
    public float[] Probabilities;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private float tempoUnit;

    public WaveData[] WaveData;

    [SerializeField] private MusicManager MusicManager;

    private Waypoint _waypoint;
    private float _spawnTimer = 0;
    private int _enemiesSpawned = 0;
    private int _currentWave = 0;
    private bool _waitingForNextWave = true;
    private int[] _typeCounters = {0, 0, 0, 0, 0};
    private bool _playing = true;

    public static event Action WaveOver;
    public static event Action Win;

    private void Start() {
        _waypoint = GetComponent<Waypoint>();
    }

    private void Update() {
        if (_playing) {
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer <= 0f && !_waitingForNextWave) {
                if (_enemiesSpawned < WaveData[_currentWave].NumEnemies) {
                    int spawnTimeUnit = UnityEngine.Random.Range(WaveData[_currentWave].MinSpawnTime, WaveData[_currentWave].MaxSpawnTime);
                    _spawnTimer = tempoUnit*spawnTimeUnit;
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
                        Win?.Invoke();
                        Done();
                    }
                }
            }
        }
    }

    private void SpawnEnemy() {
        GameObject newEnemy = Instantiate(GetEnemyChoice());
        newEnemy.transform.SetParent(transform);
        newEnemy.transform.position = _waypoint.GetWaypointPosition(0);
        Enemy enemyComponent = newEnemy.GetComponent<Enemy>();
        enemyComponent.Waypoint = _waypoint;
        newEnemy.SetActive(true);
        Enemy.EnemyType type = enemyComponent.GetEnemyType();
        _typeCounters[(int)type] = _typeCounters[(int)type] + 1;
        if (_typeCounters[(int)type] == 1) {
            MusicManager.IntroduceType(type);
        }
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

    public void DecreaseTypeCount(Enemy.EnemyType type) {
        _typeCounters[(int)type] = _typeCounters[(int)type] - 1;
        if (_typeCounters[(int)type] == 0) {
            MusicManager.TypeGone(type);
        }
    }

    private void Done() {
        _playing = false;
        // for (int i = 0; i < transform.childCount; i++) {
        //     UnityEngine.Object.Destroy(transform.GetChild(0).gameObject);
        // }
        foreach (Transform child in transform) {
            UnityEngine.Object.Destroy(child.gameObject);
        }
    }
    
    private void OnEnable() {
        LevelManager.Lose += Done;
    }

    private void OnDisable() {
        LevelManager.Lose -= Done;
    }
}
