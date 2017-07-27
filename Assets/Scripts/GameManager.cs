using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class GameManager {

    public delegate void ScoreChange(int score);
    public delegate void LivesChange(int lives);
    public delegate void damageChange(float damage);

    static public event LivesChange LivesChanged;
    static private int _lives = 3;
    static public int Lives
    {
        get { return _lives; }
        set
        {
            if (value != _lives)
            {
                _lives = value;
                if(LivesChanged != null)
                {
                    LivesChanged(_lives);
                }
                if(_lives <= 0)
                {
                    //TODO Handle Game Over
                }
            }
        }
    }

    static public event ScoreChange ScoreChanged;
    static private int _score;
    static public int Score
    {
        get { return _score; }
        set
        {
            if (value != _score)
            {
                _score = value;
                if(ScoreChanged != null)
                {
                    ScoreChanged(_score);
                }
                if(_score > HighScore)
                {
                    HighScore = _score;
                }
            }
        }
    }

    static public event ScoreChange HighScoreChanged;
    static public int HighScore
    {
        get { return PlayerPrefs.GetInt("HighScore", 0); }
        set
        {
            PlayerPrefs.SetInt("HighScore", value);
            if(HighScoreChanged != null)
            {
                HighScoreChanged(value);
            }
        }
    }

    static public event damageChange DamageChanged;
    public const float maxDamage = 100;
    static private float _damage;
    static public float Damage
    {
        get { return _damage; }
        set
        {
            if (value != _damage)
            {
                _damage = value;
                if(DamageChanged != null)
                {
                    DamageChanged(_damage);
                }
                if (_damage >= maxDamage)
                {
                    Lives--;
                    _damage = 0;
                }
            }
        }
    }


}
