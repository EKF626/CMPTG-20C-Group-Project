using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRotate : MonoBehaviour
{
    [SerializeField] private float speed = 150;

    private Tower _tower;

    private void Start() {
        _tower = transform.GetComponent<Tower>();
    }

    private void Update() {
        GameObject currentEnemy = _tower.GetCurrentEnemy();
        float angle = 0;
        if (currentEnemy) {
            angle = Mathf.Atan2(currentEnemy.transform.position.y - transform.position.y, currentEnemy.transform.position.x -transform.position.x ) * Mathf.Rad2Deg;
        }
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
}
