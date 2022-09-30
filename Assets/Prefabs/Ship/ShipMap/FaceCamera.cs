using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera shipCamera;
    public bool playerInRange;
    private GameObject[] players;
    private Transform closestPlayer;

    public float sightRange = 6;
    public float distance;
    public LayerMask people;
    public float playerdistance;
    new Renderer renderer;

    Color theColorToAdjust;


    // Start is called before the first frame update
    void Start()
    {
        //get the ship camera
        shipCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        renderer = GetComponent<Renderer>();
        theColorToAdjust = renderer.material.color;


    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt( shipCamera.transform );
        playerInRange = Physics.CheckSphere(transform.position, sightRange, people);
        players = GameObject.FindGameObjectsWithTag("Player");




        if (players.Length > 0)
        {
            closestPlayer = GetClosestEnemy(players.Select(i => i.transform).ToArray());
            distance = Vector3.Distance(transform.position, closestPlayer.position);
            //material.SetColor("_EmissionColor", new Color(46f, 49f, 191f, 1.0f) / 10 / distance);
            //theColorToAdjust.a = 0f; // Completely transparent
            //theColorToAdjust.a = 1f; // completely opaque

           
                theColorToAdjust.a = (distance < sightRange ? 1f : 0f); // Completely transparent

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
