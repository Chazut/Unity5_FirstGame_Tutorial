using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsInput : ControlsBase {

    private Vector2 _movement;

    private void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
        controllable.Move(_movement);

        controllable.Attaking = Input.GetButton("Fire1");
    }

}
