using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class ShipControllerPhysics2D : ShipControllerBase {

    public float steeringMultiplier = 5;
    public Vector2 thrustMultiplier = new Vector2(10.0f, 10.0f);

    private Rigidbody2D _rigidBody2D;
    private Vector3 _thrustDirection;

    protected override void Awake()
    {
        base.Awake();
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidBody2D.AddTorque(-Steering*steeringMultiplier, ForceMode2D.Force);
        _thrustDirection = (Vector3)Thrust;
        _thrustDirection.Scale(thrustMultiplier);
        _rigidBody2D.AddRelativeForce(_thrustDirection, ForceMode2D.Force);
    }

    public override void ResetShip()
    {
        base.ResetShip();
        _rigidBody2D.velocity = Vector2.zero;
        _rigidBody2D.angularVelocity = 0;
    }

}
