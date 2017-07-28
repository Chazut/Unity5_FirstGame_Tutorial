using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fire a projectile.
/// </summary>
[AddComponentMenu("Chazu Games/Projectile")]
public class Projectile : MonoBehaviour {

    public float speed = 0.5f;
    [System.NonSerialized]
    public float range = 5;

    private float _distance;
    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _distance = 0;
        if (_audio)
            _audio.Play();
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0, speed * Time.fixedDeltaTime, 0), Space.Self);
        _distance += speed * Time.fixedDeltaTime;
        if(_distance > range)
        {
            //Destroy(gameObject);
            ObjectPool.Release(gameObject);
        }
    }

}
