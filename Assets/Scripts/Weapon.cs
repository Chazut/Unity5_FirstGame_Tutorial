using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Weapon.
/// </summary>
[AddComponentMenu("Chazu Games/Weapon")]
public class Weapon : MonoBehaviour {

    public GameObject projectile;
    private int _projectileID;
    public Transform[] emmitters;
    [Range(0.001f, 10f)] public float firingRate = 1f;
    public float firingRange = 5;

    private int _current;
    private Collider2D _shipCollider2D;

    private void Awake()
    {
        _shipCollider2D = transform.parent.GetComponent<Collider2D>();
        ObjectPool.InitPool(projectile);
        _projectileID = projectile.GetInstanceID();
    }

    private void Start()
    {
        
    }

    private void Fire()
    {
        _current = (_current >= emmitters.Length - 1) ? 0 : _current + 1;
        Vector3 position = emmitters[_current].TransformPoint(Vector3.up * 0.5f);
        //GameObject projectileInstance = (GameObject) Instantiate(projectile, position, emmitters[_current].rotation);
        GameObject projectileInstance = ObjectPool.GetInstance(_projectileID, position, emmitters[_current].rotation);
        projectileInstance.GetComponent<Projectile>().range = firingRange;
        Physics2D.IgnoreCollision(_shipCollider2D, projectileInstance.GetComponent<Collider2D>());
    }

}
