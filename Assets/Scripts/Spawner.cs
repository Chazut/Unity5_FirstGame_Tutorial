using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawn an Object into an Area, with customizable velocity and location
/// </summary>
[AddComponentMenu("Chazu Games/Spawner")]
public class Spawner : MonoBehaviour {

    [Header("SPAWN")]
    public GameObject reference;

    [Header("SPAWNING")]
    [Range(0.001f, 100f)] public float minRate = 0.50f;
    [Range(0.001f, 100f)] public float maxRate = 3.0f;
    public int number;
    public bool infinite;

    private int _remaining;

    [Header("LOCATIONS")]
    public GameArea area;
    private Transform player;
    public float minDistanceFromPlayer;

    [Header("VELOCITY")]
    [Range(-180f, 180f)] public float angle;
    [Range(0, 360f)] public float spread = 30f;
    [Range(0, 10)] public float minStrenght = 1f;
    [Range(0, 10f)] public float maxStrenght = 10f;

    [Header("ANIMATOR")]
    public string animatorSpawningParameterName = "Spawning";
    private int spawningHashID;
    public float animatorDelayIn = 3;
    public float animatorDelayOut = 1;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator)
            spawningHashID = Animator.StringToHash(animatorSpawningParameterName);
    }

    private IEnumerator Start()
    {
      
        _remaining = number;
        if(minDistanceFromPlayer > 0)
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO)
                player = playerGO.transform;
            else
                Debug.LogWarning("No Player Found.");
        }

        if (animator)
        {
            animator.SetBool(spawningHashID, true);
            yield return new WaitForSeconds(animatorDelayIn);
        }

        while (infinite || _remaining > 0)
        {
            Vector3 _position = area ? area.GetRandomPosition() : transform.position;

            if(player && Vector3.Distance(_position, player.position) < minDistanceFromPlayer)
            {
                Vector3 debugPos = _position;
                Debug.DrawLine(transform.position, debugPos);
                _position = (_position - player.position).normalized * minDistanceFromPlayer;
                Debug.DrawLine(debugPos, _position);
            }

            // TODO : Use Object Pulling
            GameObject obj = (GameObject) Instantiate(reference, _position, transform.rotation);
            Rigidbody2D rb2D = obj.GetComponent<Rigidbody2D>();
            if (rb2D)
            {
                float angleDelta = Random.Range(-spread * 0.5f, spread * 0.5f);
                float angle_ = angle + angleDelta;
                Vector2 direction = new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle_), Mathf.Cos(Mathf.Deg2Rad * angle_));
                direction *= Random.Range(minStrenght, maxStrenght);
                rb2D.velocity = direction;
            }

            _remaining--;
            yield return new WaitForSeconds(1 / Random.Range(minRate, maxRate));
        }

        if (animator)
        {
            yield return new WaitForSeconds(animatorDelayOut);
            animator.SetBool(spawningHashID, false);
        }

        gameObject.SetActive(false);
    }
}
