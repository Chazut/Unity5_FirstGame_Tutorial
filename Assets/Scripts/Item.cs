using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ?
/// </summary>
[AddComponentMenu("Chazu Games/Item")]
public class Item : MonoBehaviour {

	public enum TYPE { RepairKit, ExtraLife, Weapon};
    public TYPE type;

    public Weapon weapon;

    private AudioSource audioSrc;
    private Renderer _renderer;
    private Collider2D _collider2D;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        _renderer = GetComponent<Renderer>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
            return;

        switch (type)
        {
            case TYPE.RepairKit:
                GameManager.Damage = 0;
                break;
            case TYPE.ExtraLife:
                GameManager.Lives++;
                break;
            case TYPE.Weapon:
                other.GetComponent<ShipControllerBase>().SwitchWeapon(weapon);
                break;
            default:
                break;
        }

        StartCoroutine(PickUp());
    }

    private IEnumerator PickUp()
    {
        audioSrc.Play();
        _collider2D.enabled = false;
        _renderer.enabled = false;
        yield return new WaitForSeconds(audioSrc.clip.length);

        //Destroy(gameObject);
        ObjectPool.Release(gameObject);
    }
}
