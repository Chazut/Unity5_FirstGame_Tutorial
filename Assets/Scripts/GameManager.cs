using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class GameManager {

    public const float maxDamage = 100;

    static private float _damage;
    static public float Damage
    {
        get { return _damage; }
        set {
            if (value != _damage)
            {
                _damage = value;
                if (_damage >= maxDamage)
                {
                    //TODO : Lives --
                    _damage = 0;
                }
                Debug.Log(value);
            }
        }
    }


}
