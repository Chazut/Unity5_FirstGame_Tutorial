using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Deal damage to the Ship.
/// </summary>
[AddComponentMenu("Chazu Games/Ship Damage")]
public class ShipDamage : MonoBehaviour {

    public float vulnerability = 1;

    private Rigidbody2D _rigidBody2D;
    private Collider2D _collider2D;

    public GameObject impact;
    private int _impactID;

    private float Damage
    {
        get{ return GameManager.Damage; }
        set { GameManager.Damage = value; }
    }

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();

        _impactID = impact.GetInstanceID();
        ObjectPool.InitPool(impact);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float damage = collision.relativeVelocity.magnitude * vulnerability;
        //if(collision.collider.sharedMaterial.name == "Asteroid")
        if (collision.collider.sharedMaterial)
        {
            damage *= (1 / collision.collider.sharedMaterial.bounciness) 
                * collision.collider.sharedMaterial.friction 
                * (1/_collider2D.sharedMaterial.bounciness)
                * _collider2D.sharedMaterial.friction;
        }

        if (collision.rigidbody)
        {
            damage *= (collision.rigidbody.mass / _rigidBody2D.mass);
        }

        if (damage >= 0.01f)
        {
            GameObject impactObj = ObjectPool.GetInstance(_impactID, collision.contacts[0].point);
            impactObj.transform.parent = transform;
        }

        Damage += damage;
    }

}
