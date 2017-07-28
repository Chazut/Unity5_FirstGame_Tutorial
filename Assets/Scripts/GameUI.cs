using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// GameUI.
/// </summary>
[AddComponentMenu("Chazu Games/GameUI")]
public class GameUI : MonoBehaviour {

    [Header("HUD")]
    public Text livesText;
    public Text scoreText;
    public Text highScore;
    public Slider damageSlider;
    private Image _damageFillArea;
    public Color damageSliderColorMin = Color.yellow;
    public Color damageSliderColorMax = Color.red;

    [Header("NAV")]
    public Text gameStateText;
    public Button pauseButton;
    public Button resumeButton;
    public Image pauseMenu;

    [Header("Settings")]
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    public float MusicVolume
    {
        get { return PlayerSettings.MusicVolume; }
        set { PlayerSettings.MusicVolume = value; }
    }

    public float SfxVolume
    {
        get { return PlayerSettings.SfxVolume; }
        set { PlayerSettings.SfxVolume = value; }
    }

    private void Awake()
    {
        damageSliderColorMin = Color.yellow;
        damageSliderColorMax = Color.red;
        damageSlider.maxValue = GameManager.maxDamage;
        _damageFillArea = damageSlider.fillRect.GetComponent<Image>();

        musicVolumeSlider.value = MusicVolume;
        sfxVolumeSlider.value = SfxVolume;
    }

    private void Start()
    {
        livesText.text = string.Format("{1} {0}", GameManager.Lives > 1 ? "LIVES" : "LIFE", GameManager.Lives.ToString());
        scoreText.text = string.Format("SCORE : {0}", GameManager.Score);
        highScore.text = string.Format("HIGH : {0}", GameManager.HighScore);
        damageSlider.value = GameManager.Damage;
        _damageFillArea.color = Color.Lerp(damageSliderColorMin, damageSliderColorMax, GameManager.Damage / damageSlider.maxValue);

        OnStateChanged(GameManager.State);

        GameManager.ScoreChanged += delegate (int score)
            {
                scoreText.text = string.Format("SCORE : {0}", score);
            };

        GameManager.HighScoreChanged += delegate (int score)
            {
                highScore.text = string.Format("HIGH : {0}", score);
            };

        GameManager.LivesChanged += delegate (int lives)
            {
                livesText.text = string.Format("{1} {0}", lives > 1 ? "LIVES" : "LIFE", lives.ToString());
            };

        GameManager.DamageChanged += delegate (float damage)
            {
                damageSlider.value = damage;
                _damageFillArea.color = Color.Lerp(damageSliderColorMin, damageSliderColorMax, damage/damageSlider.maxValue);
            };

        GameManager.StateChanged += OnStateChanged;
    }

    void OnStateChanged(GameManager.STATE state)
    {
        gameStateText.text = string.Format("GAME {0}", state.ToString().ToUpper());
        pauseButton.gameObject.SetActive(state == GameManager.STATE.Running);
        pauseMenu.gameObject.SetActive(state != GameManager.STATE.Running);
        resumeButton.gameObject.SetActive(state != GameManager.STATE.Over);
    }

    public void PauseGame()
    {
        GameManager.State = GameManager.STATE.Paused;
    }

    public void ResumeGame()
    {
        GameManager.State = GameManager.STATE.Running;
    }

    public void RestartGame()
    {
        GameManager.Restart();
    }

}
