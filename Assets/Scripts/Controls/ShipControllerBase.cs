using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Animator))]
public abstract class ShipControllerBase : MonoBehaviour, IControllable
{
    public AnimationCurve steeringCurve;
    public AnimationCurve glideCurve;

    private float rearPowerLimit = 0.2f;
    private Vector2 _thrust_tmp;

    protected Animator _animatorController;
    protected int _steeringHashID;
    protected int _thrustXHashID;
    protected int _thrustYHashID;

    private Vector2 _thrust;
    protected Vector2 Thrust
    {
        get { return _thrust; }
        set
        {
            if (value != _thrust)
            {
                _thrust = value;
                _animatorController.SetFloat(_thrustXHashID, _thrust.x);
                _animatorController.SetFloat(_thrustYHashID, _thrust.y);
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
                _animatorController.SetFloat(_steeringHashID, _steering);
            }
        }
    }


    public virtual void Move(Vector2 movement)
    {
        movement = Vector2.ClampMagnitude(movement, 1.0f);
        movement.y = Mathf.Clamp(movement.y, -rearPowerLimit, 1);

        //Steering = movement.x;
        Steering = steeringCurve.Evaluate(movement.y) * movement.x;

        _thrust_tmp.x = glideCurve.Evaluate(movement.y) * movement.x;
        _thrust_tmp.y = movement.y;
        Thrust = _thrust_tmp;

        enabled = (movement != Vector2.zero);
    }

    protected void Awake()
    {
        _animatorController = GetComponent<Animator>();
        _steeringHashID = Animator.StringToHash("Steering");
        _thrustXHashID = Animator.StringToHash("ThrustX");
        _thrustYHashID = Animator.StringToHash("ThrustY");
    }

}
