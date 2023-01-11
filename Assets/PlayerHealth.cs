using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public PlayerController controller;
    public Vector3 spawnLocation;

    //we need a health bar

    //we need a way to adjust the health bar



    // Start is called before the first frame update
    public void Start()
    {
        spawnLocation = GameObject.Find ("Spawn").transform.position;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health < 0)
        {
            controller.transform.position = spawnLocation;
        }
    }
}
