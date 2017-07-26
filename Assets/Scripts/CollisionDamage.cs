using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Define damage.
/// </summary>
[AddComponentMenu("Chazu Games/Collision Damage")]
public class CollisionDamage : MonoBehaviour {

    public float damageAmount;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        collision.gameObject.SendMessage("ApplyDamage", damageAmount, SendMessageOptions.DontRequireReceiver);
    }

}
