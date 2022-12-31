using ProjectDawn.SplitScreen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ship : MonoBehaviour
{
    Rigidbody rb;
    public Slider health;
    public GameObject explosion;
    public Transform explosionOrigin;
    public float explosionForce;
    public GameObject explosionParent;
    public GameObject splitScreenGameObject;


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

        //we need to unparent the split screen prior to destroying the ship so
        //that we still have a cam rendering.
        UnparentSplitScreen();

        //Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        //var rbs = explosionParent.GetComponentsInChildren<MeshCollider>();
        //for (var i = 0; i < rbs.Length; i++)
        //{
        //    //UnityEngine.Gizmos.DrawSphere(gameObject.transform.position, 50);
        //    rbs[i].gameObject.AddComponent<Rigidbody>();
        //}

        //var objects = explosionParent.GetComponentsInChildren<Rigidbody>();
        //for(var i=0; i < objects.Length; i++)
        //{
        //    //UnityEngine.Gizmos.DrawSphere(gameObject.transform.position, 50);
        //    objects[i].AddExplosionForce(explosionForce, explosionOrigin.position, 50);
        //}
        Destroy(gameObject,3);

    }
    private void UnparentSplitScreen()
    {
        splitScreenGameObject.transform.parent = null;

        var ss = splitScreenGameObject.GetComponent<SplitScreenEffect>();
        ss.Screens.ForEach(s =>
        {
            s.Camera.transform.parent = null;
            s.Target.transform.parent = null;
        });
    }
}
