using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private void Start()
    {
        //Called when the scene is loaded   <----
    }

    private void Update()
    {
        //Called every frame  <----
    }

    private void IntroduceType(Enemy.EnemyType type) {
        switch(type) {
            case Enemy.EnemyType.PacMan:
                // Start PacMan theme  <----
                break;
            case Enemy.EnemyType.Sonic:
                // Start Sonic eheme  <----
                break;
            case Enemy.EnemyType.Mario:
                // Start Mario theme  <----
                break;
            case Enemy.EnemyType.Pokemon:
                // Start Pokemon theme  <----
                break;
        }
    }

    private void TypeGone(Enemy.EnemyType type) {
       switch(type) {
            case Enemy.EnemyType.PacMan:
                // End PacMan theme  <----
                break;
            case Enemy.EnemyType.Sonic:
                // End Sonic eheme  <----
                break;
            case Enemy.EnemyType.Mario:
                // End Mario theme  <----
                break;
            case Enemy.EnemyType.Pokemon:
                // End Pokemon theme  <----
                break;
        }
    }

    private void onEnable() {
        Spawner.IntroduceType += IntroduceType;
        Spawner.TypeGone += TypeGone;
    }

    private void OnDisable () {
        Spawner.IntroduceType -= IntroduceType;
        Spawner.TypeGone -= TypeGone;
    }
}
