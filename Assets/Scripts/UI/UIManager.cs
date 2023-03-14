using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject Path;
    [SerializeField] private GameObject LevelManager;
    [SerializeField] private GameObject WaveText;
    [SerializeField] private GameObject CoinsText;
    [SerializeField] private GameObject LivesText;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject[] PurchaseButtons;

    private Spawner _spawner;
    private LevelManager _levelManager;
    private TextMeshProUGUI _waveText;
    private TextMeshProUGUI _coinsText;
    private TextMeshProUGUI _livesText;

    private void Start() {
        _spawner = Path.GetComponent<Spawner>();
        _levelManager = LevelManager.GetComponent<LevelManager>();
        _waveText = WaveText.GetComponent<TextMeshProUGUI>();
        _coinsText = CoinsText.GetComponent<TextMeshProUGUI>();
        _livesText = LivesText.GetComponent<TextMeshProUGUI>();

        HideButtons();
        ShowButtons();
    }

    private void Update() {
        _waveText.text = ("Wave " + (_spawner.GetWaveNumber()+1) + "/" + _spawner.WaveData.Length);
        _coinsText.text = ("Bytes: " + _levelManager.GetCoins());
        _livesText.text = ("Lives: " + _levelManager.GetLives());
    }

    public void ShowButtons() {
        StartButton.SetActive(true);
        for (int i = 0; i < PurchaseButtons.Length; i++) {
            if (PurchaseButtons[i].GetComponent<PurchaseTowerButton>().GetLevelAvalible() <= _spawner.GetWaveNumber()+1) {
                PurchaseButtons[i].SetActive(true);
            }
        }
    }

    public void HideButtons() {
        StartButton.SetActive(false);
        for (int i = 0; i < PurchaseButtons.Length; i++) {
            PurchaseButtons[i].SetActive(false);
        }
    }

    private void OnEnable() {
        Spawner.WaveOver += ShowButtons;
    }

    private void OnDisable() {
        Spawner.WaveOver -= ShowButtons;
    }
}
