using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            Instantiate(reference, _position, transform.rotation);
            _remaining--;
            yield return new WaitForSeconds(1 / Random.Range(minRate, maxRate));
        }
    }
}
