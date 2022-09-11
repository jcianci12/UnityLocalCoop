using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        var EnemyAIScript = other.gameObject.GetComponent<EnemyAI>();
        if(EnemyAIScript != null)
        {

            Debug.Log("we hit a enemy");
            EnemyAIScript.TakeDamage(3);

        }

    }
}
