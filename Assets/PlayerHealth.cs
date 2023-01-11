using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public PlayerController controller;
    public GameObject spawnObject;
    public Vector3 spawnLocation;
    public Slider slider;


    //we need a health bar

    //we need a way to adjust the health bar



    // Start is called before the first frame update
    public void Start()
    {
        spawnLocation = GameObject.Find (spawnObject.name).transform.position;
    }
    public void TakeDamage(int damage)
    {
        slider.value -= damage;
        if(slider.value < 0)
        {
            controller.transform.position = spawnLocation;
        }
    }
}
