using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipMap : MonoBehaviour
{
    public float sightRange;
    public bool playerInRange;
    private GameObject[] players;
    private Transform closestPlayer;
    public float distance;
    public LayerMask people;
    public Material material;
    public float playerdistance;
    //Check for sight and attack range
    private void Update()
    {
        playerInRange = Physics.CheckSphere(transform.position, sightRange, people);
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            closestPlayer = GetClosestEnemy(players.Select(i => i.transform).ToArray());
            distance = Vector3.Distance(transform.position, closestPlayer.position);
            material.SetColor("_EmissionColor", new Color(46f, 49f, 191f, 1.0f) /10/ distance);

        }

    }
    Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

}
