using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Asteroid.
/// </summary>
[AddComponentMenu("Chazu Games/Asteroid")]
public class Asteroid : MonoBehaviour {

    public int score = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Projectile")
            return;

        GameManager.Score += score;

        //TODO Use Object Pooling
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }

}
