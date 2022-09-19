using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera shipCamera;
    private bool playerInRange;
    private GameObject[] players;
    private Transform closestPlayer;

    public float sightRange = 1;
    public float distance;
    public LayerMask people;
    public float playerdistance;

    Color theColorToAdjust;

    // Start is called before the first frame update
    void Start()
    {
        //get the ship camera
        shipCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        theColorToAdjust = gameObject.GetComponent<Material>().color;

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt( shipCamera.transform );
        playerInRange = Physics.CheckSphere(transform.position, sightRange, people);
        players = GameObject.FindGameObjectsWithTag("Player");


        theColorToAdjust.a = (playerInRange?1f:0f); // Completely transparent


        //if (players.Length > 0)
        //{
        //    closestPlayer = GetClosestEnemy(players.Select(i => i.transform).ToArray());
        //    distance = Vector3.Distance(transform.position, closestPlayer.position);
        //    //material.SetColor("_EmissionColor", new Color(46f, 49f, 191f, 1.0f) / 10 / distance);
        //    theColorToAdjust.a = 0f; // Completely transparent
        //    //theColorToAdjust.a = 1f; // completely opaque
        //}
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
