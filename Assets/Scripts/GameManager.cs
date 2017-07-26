using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class GameManager {

    static private int _lives = 3;
    static public int Lives
    {
        get { return _lives; }
        set
        {
            if (value != _lives)
            {
                _lives = value;
                if(_lives <= 0)
                {
                    //TODO Handle Game Over
                }
            }
        }
    }


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
                if (_damage >= maxDamage)
                {
                    Lives--;
                    _damage = 0;
                }
            }
        }
    }


}
