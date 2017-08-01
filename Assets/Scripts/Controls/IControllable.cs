using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable {

    bool Attaking { get; set; }
    void Move(Vector2 movement);

}
