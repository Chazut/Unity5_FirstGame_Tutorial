using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    protected int _shieldHashID;

    private List<Weapon> _weapons;
    private Weapon _weapon;
    public Weapon Weapon
    {
        get
        {
            if(!_weapon)
                _weapon = GetComponentInChildren<Weapon>();
            return _weapon;
        }
        set
        {
            if (value != _weapon)
            {
                _weapon = value;
                if (!_weapons.Contains(_weapon))
                    _weapons.Add(_weapon);
            }
        }
    }


    public bool Attaking
    {
        get
        {
            return Firing;
        }

        set
        {
            Firing = value;
        }
    }
    public bool Protecting
    {
        get
        {
            return Shield;
        }

        set
        {
            Shield = value;
        }
    }

    private bool _shield;
    protected bool Shield
    {
        get
        {
            return _shield;
        }

        set
        {
            if(value != _shield)
            {
                _shield = value;
                _animatorController.SetLayerWeight(_shieldHashID, _shield ? 1f : 0f);
            }
            if (_shield)
                Firing = false;
        }
    }

    private bool _firing;
    protected bool Firing
    {
        get { return _weapon.Firing; }
        set { _weapon.Firing = value; }
    }

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

    public virtual void Move (Vector2 movement)
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

    public void SwitchWeapon (Weapon newWeapon)
    {
        if (Weapon.name == newWeapon.name)
            return;

        bool wasFiring = Firing;
        Firing = false;
        Weapon.gameObject.SetActive(false);

        var existingWeapon = (from item in _weapons
                              where item.name == newWeapon.name
                              select item).FirstOrDefault();

        if (existingWeapon != null)
        {
            existingWeapon.gameObject.SetActive(true);
            Weapon = existingWeapon;
        }
        else
        {
            GameObject newWeaponGO = Instantiate(newWeapon.gameObject);
            newWeaponGO.transform.parent = transform;
            newWeaponGO.transform.localPosition = Vector3.zero;
            newWeaponGO.transform.localRotation = Quaternion.identity;
            Weapon = newWeaponGO.GetComponent<Weapon>();
            Weapon.gameObject.SetActive(true);
        }

        Firing = wasFiring;
    }

    public virtual void ResetShip()
    {
        Steering = 0;
        Thrust = Vector2.zero;
        Firing = Shield = false;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        gameObject.SendMessage("Repair");

        SwitchWeapon(_weapons[0]);
        _weapons = new List<Weapon>();
    }

    protected virtual void Awake()
    {
        _animatorController = GetComponent<Animator>();

        _steeringHashID = Animator.StringToHash("Steering");
        _thrustXHashID = Animator.StringToHash("ThrustX");
        _thrustYHashID = Animator.StringToHash("ThrustY");

        _shieldHashID = _animatorController.GetLayerIndex("Shield");
        if (_shieldHashID == -1)
            Debug.LogWarning("Animator controller must have a 'Shield' layer!");

        _weapons = new List<Weapon>();
        _weapons.Add(Weapon);
    }

}
