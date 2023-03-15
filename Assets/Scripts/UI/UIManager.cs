using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject Path;
    [SerializeField] private GameObject LevelManager_;
    [SerializeField] private GameObject WaveText;
    [SerializeField] private GameObject CoinsText;
    [SerializeField] private GameObject LivesText;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject WinMessage;
    [SerializeField] private GameObject LossMessage;
    [SerializeField] private GameObject BackToMenuButton;
    [SerializeField] private GameObject[] PurchaseButtons;

    private Spawner _spawner;
    private LevelManager _levelManager;
    private TextMeshProUGUI _waveText;
    private TextMeshProUGUI _coinsText;
    private TextMeshProUGUI _livesText;

    private void Start() {
        _spawner = Path.GetComponent<Spawner>();
        _levelManager = LevelManager_.GetComponent<LevelManager>();
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

    private void ShowButtons() {
        StartButton.SetActive(true);
        for (int i = 0; i < PurchaseButtons.Length; i++) {
            if (PurchaseButtons[i].GetComponent<PurchaseTowerButton>().GetLevelAvalible() <= _spawner.GetWaveNumber()+1) {
                PurchaseButtons[i].SetActive(true);
            }
        }
    }

    private void HideButtons() {
        StartButton.SetActive(false);
        for (int i = 0; i < PurchaseButtons.Length; i++) {
            PurchaseButtons[i].SetActive(false);
        }
    }

    private void ShowWin() {
        WinMessage.SetActive(true);
        BackToMenuButton.SetActive(true);
    }

    private void ShowLoss() {
        LossMessage.SetActive(true);
        BackToMenuButton.SetActive(true);
    }

    private void OnEnable() {
        Spawner.WaveOver += ShowButtons;
        Spawner.Win += ShowWin;
        LevelManager.Lose += ShowLoss;
    }

    private void OnDisable() {
        Spawner.WaveOver -= ShowButtons;
        Spawner.Win -= ShowWin;
        LevelManager.Lose -= ShowLoss;
    }
}
