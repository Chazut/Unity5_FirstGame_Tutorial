using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SpaceShip controller.
/// </summary>
[AddComponentMenu("Chazu Games/SpaceShip Controller")]
public class SimpleShipController : MonoBehaviour {

    public float thrustPower = 1;
    public float steerPower = 1;

    private Vector2 delta = Vector2.zero;
    private Rigidbody2D _rigidBody2D;
    private Vector2 _force = Vector2.zero;
    private float _torque;
    private Weapon _weapon;

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _weapon = GetComponentInChildren<Weapon>();
    }

    private bool _firing;
    public bool Firing
    {
        get { return _firing; }
        set
        {
            if (value != _firing)
            {
                _firing = value;
                if (_firing)
                {
                    _weapon.InvokeRepeating("Fire", (1f/_weapon.firingRate), (1f / _weapon.firingRate));
                } else
                {
                    _weapon.CancelInvoke();
                }
            }
        }
    }


    // Update is called once per frame
    void FixedUpdate () {
        delta.x = Input.GetAxis("Horizontal");
        delta.y = Input.GetAxis("Vertical");

        Firing = Input.GetButton("Fire1");

        _force.y = delta.y * thrustPower;
        _torque = -delta.x * steerPower;

        _rigidBody2D.AddRelativeForce(_force);
        _rigidBody2D.AddTorque(_torque);
	}
}
