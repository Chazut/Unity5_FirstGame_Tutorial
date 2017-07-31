using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControlsBase : MonoBehaviour {

    protected IControllable controllable;
    public ShipControllerBase shipController;

    protected void Awake()
    {
        controllable = shipController;
    }

}
