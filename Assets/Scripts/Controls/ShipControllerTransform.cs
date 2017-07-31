using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControllerTransform : ShipControllerBase {

    public float steeringMultiplier = 5;
    public Vector2 thrustMultiplier = new Vector2(0.2f, 0.25f);

    private Vector3 _thrustDirection;

    private void Update()
    {
        _thrustDirection = (Vector3)Thrust;
        _thrustDirection.Scale(thrustMultiplier);
        transform.Translate(_thrustDirection, Space.Self);
        transform.Rotate(0, 0, -Steering * steeringMultiplier, Space.Self);
    }

}
