using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Asteroid.
/// </summary>
[AddComponentMenu("Chazu Games/Asteroid")]
public class Asteroid : MonoBehaviour {

    public int score = 5;

    public GameObject explosion;
    private int _explosionID;
    private float _delay;

    private Renderer _renderer;
    private Collider2D _collider2D;

    private void Awake()
    {
        _explosionID = explosion.GetInstanceID();
        ObjectPool.InitPool(explosion);
        _delay = explosion.GetComponent<AutoRelease>().duration;

        _collider2D = GetComponent<Collider2D>();
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _collider2D.enabled = true;
        _renderer.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Projectile")
            return;

        GameManager.Score += score;

        _collider2D.enabled = false;
        _renderer.enabled = false;

        ObjectPool.Release(collision.gameObject);

        GameObject expl = ObjectPool.GetInstance(_explosionID, collision.contacts[0].point);
        expl.GetComponent<Follow>().target = transform;
        //expl.transform.parent = transform;

        //Destroy(gameObject);
        //Destroy(collision.gameObject);
        ObjectPool.Release(gameObject);

        _delay = expl.GetComponent<AutoRelease>().duration;
        StartCoroutine(Release(_delay));
    }

    private IEnumerator Release(float delay)
    {
        yield return new WaitForSeconds(delay);
        //ObjectPool.Release(gameObject);
        StartCoroutine(Release(delay));
    }

}
