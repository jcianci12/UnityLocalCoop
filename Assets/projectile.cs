using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sparks;
    public float velocity;
    public int damage;
    
    public float TimeToLive;
    void Start()
    {
        Destroy(gameObject, TimeToLive);
    }

    private void OnCollisionEnter(Collision other)

    {
            
        var EnemyAIScript = other.gameObject.GetComponent<EnemyAI>();
        if(EnemyAIScript != null)
        {

            Debug.Log("we hit a enemy");
            foreach (ContactPoint contact in other.contacts)
            {
                Debug.DrawRay(contact.point, contact.normal, Color.white);
                Instantiate(sparks, contact.point, Quaternion.FromToRotation(contact.point,contact.normal));
            }
            EnemyAIScript.TakeDamage(damage);
            Destroy(gameObject);
        }
        if(other.gameObject.GetComponent<ship>() != null)
        {
            other.gameObject.GetComponent<ship>().TakeDamage(damage);
        }

    }
}
