using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseTowerButton : MonoBehaviour
{
    [SerializeField] private GameObject Tower;
    [SerializeField] private int Cost;
    [SerializeField] private int LevelAvalible;
    [SerializeField] private GameObject[] TowerSlots;
    [SerializeField] private GameObject LevelManager;
    [SerializeField] private GameObject TowerImage;

    private bool _placing = false;
    private LevelManager _levelManager;

    private void Start() {
        _levelManager = LevelManager.GetComponent<LevelManager>();
    }

    private void Update() {
        if (_placing) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            TowerImage.transform.position = mousePosition;
            if (Input.GetMouseButtonDown(0))  {
                for (int i = 0; i < TowerSlots.Length; i++) {
                    if ((mousePosition - TowerSlots[i].transform.position).magnitude < 0.5f && !TowerSlots[i].GetComponent<TowerSlot>().IsTaken()) {
                        _placing = false;
                        TowerImage.SetActive(false);
                        _levelManager.ChangeCoins(-Cost);
                        PlaceTower(TowerSlots[i]);
                        break;
                    }
                }
            }
        }
    }

    public void OnClick() {
        if (_placing) {
            _placing = false;
            TowerImage.SetActive(false);
        }
        else if (_levelManager.GetCoins() >= Cost) {
            _placing = true;
            TowerImage.SetActive(true);
        }
    }

    private void PlaceTower(GameObject towerSlot) {
        TowerSlot towerSlotComponent = towerSlot.GetComponent<TowerSlot>();
        towerSlotComponent.SetTaken(true);
        GameObject newTower = Instantiate(Tower);
        newTower.transform.position = towerSlot.transform.position;
        newTower.GetComponent<Tower>().SetTowerSlot(towerSlotComponent);
    }

    public int GetLevelAvalible() {
        return LevelAvalible;
    }
}
