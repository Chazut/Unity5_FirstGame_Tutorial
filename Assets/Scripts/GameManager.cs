using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class GameManager {

    public enum STATE
    {
        Running,
        Paused,
        Over
    }

    public delegate void ScoreChange(int score);
    public delegate void LivesChange(int lives);
    public delegate void damageChange(float damage);
    public delegate void StateChange(STATE State);

    static public event StateChange StateChanged;
    static private STATE _state;
    static public STATE State
    {
        get { return _state; }
        set
        {
            if (value != _state)
            {
                _state = value;

                switch (_state)
                {
                    case STATE.Running:
                        Time.timeScale = 1;
                        break;
                    case STATE.Paused:
                        Time.timeScale = 0;
                        break;
                    case STATE.Over:
                        Time.timeScale = 0;
                        break;
                    default:
                        break;
                }

                if (StateChanged != null)
                    StateChanged(_state);
            }
        }
    }

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
                    State = STATE.Over;
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
