using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipControllerBase : MonoBehaviour, IControllable
{

    private Vector2 _thrust;
    protected Vector2 Thrust
    {
        get { return _thrust; }
        set
        {
            if (value != _thrust)
            {
                _thrust = value;
            }
        }
    }

    private float _steering;
    protected float Steering
    {
        get { return _steering; }
        set
        {
            if (value != _steering)
            {
                _steering = value;
            }
        }
    }


    public virtual void Move(Vector2 movement)
    {
        Steering = movement.x;
        Thrust = movement;

        enabled = (movement != Vector2.zero);
    }

}
