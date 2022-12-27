using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ship : MonoBehaviour
{
    Rigidbody rb;
    public Slider health;
    public GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
        rb.inertiaTensorRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY;
    }
    private void FixedUpdate()
    {
        
    }
    public void TakeDamage(int damage)
    {
        health.value -= damage;
        

        if (health.value <= 0) Invoke(nameof(Destroy), 0.5f);
    }
    private void Destroy()
    {
        Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);

        Destroy(gameObject);

    }
}
