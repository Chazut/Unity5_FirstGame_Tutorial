using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SpaceShip controller.
/// </summary>
[AddComponentMenu("Chazu Games/SpaceShip Controller")]
public class SimpleShipController : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        transform.Translate(0, y*0.2f, 0);
        transform.Rotate(0, 0, -x*2.5f);
	}
}
