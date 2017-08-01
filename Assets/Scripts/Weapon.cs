using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Weapon.
/// </summary>
[AddComponentMenu("Chazu Games/Weapon")]
public class Weapon : MonoBehaviour {

    public new string name;
    public GameObject projectile;
    private int _projectileID;
    public Transform[] emmitters;
    [Range(0.001f, 10f)] public float firingRate = 1f;
    public float firingRange = 5;

    private int _current;
    private Collider2D _shipCollider2D;

    private bool _firing;
    public bool Firing
    {
        get { return _firing; }
        set
        {
            if (value != _firing)
            {
                _firing = value;
                if (_firing)
                {
                    InvokeRepeating("Fire", (1f / firingRate), (1f / firingRate));
                }
                else
                {
                    CancelInvoke();
                }
            }
        }
    }

    private void Awake()
    {
        if (name == "")
            name = gameObject.name;
        //FIXME
        //_shipCollider2D = transform.parent.GetComponent<Collider2D>();
        ObjectPool.InitPool(projectile);
        _projectileID = projectile.GetInstanceID();
    }

    private void Fire()
    {
        _current = (_current >= emmitters.Length - 1) ? 0 : _current + 1;
        Vector3 position = emmitters[_current].TransformPoint(Vector3.up * 0.5f);
        //GameObject projectileInstance = (GameObject) Instantiate(projectile, position, emmitters[_current].rotation);
        GameObject projectileInstance = ObjectPool.GetInstance(_projectileID, position, emmitters[_current].rotation);
        projectileInstance.GetComponent<Projectile>().range = firingRange;
        //Physics2D.IgnoreCollision(_shipCollider2D, projectileInstance.GetComponent<Collider2D>());
        foreach(Collider2D c in transform.parent.GetComponentsInChildren<Collider2D>(true))
        {
            Physics2D.IgnoreCollision(c, projectileInstance.GetComponent<Collider2D>());
        }
    }

}
