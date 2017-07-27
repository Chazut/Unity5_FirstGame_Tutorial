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

    private void Awake()
    {
        damageSliderColorMin = Color.yellow;
        damageSliderColorMax = Color.red;
        damageSlider.maxValue = GameManager.maxDamage;
        _damageFillArea = damageSlider.fillRect.GetComponent<Image>();
    }

    private void Start()
    {
        livesText.text = string.Format("{0} : {1}", GameManager.Lives > 1 ? "LIVES" : "LIFE", GameManager.Lives.ToString());
        scoreText.text = string.Format("SCORE : {0}", GameManager.Score);
        highScore.text = string.Format("HIGH : {0}", GameManager.HighScore);
        damageSlider.value = GameManager.Damage;
        _damageFillArea.color = Color.Lerp(damageSliderColorMin, damageSliderColorMax, GameManager.Damage / damageSlider.maxValue);


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
                livesText.text = string.Format("{0} : {1}", lives > 1 ? "LIVES" : "LIFE", lives.ToString());
            };

        GameManager.DamageChanged += delegate (float damage)
            {
                damageSlider.value = damage;
                _damageFillArea.color = Color.Lerp(damageSliderColorMin, damageSliderColorMax, damage/damageSlider.maxValue);
            };
    }
}
