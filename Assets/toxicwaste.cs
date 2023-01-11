using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toxicwaste : MonoBehaviour
{
    public int damage;
    public float attackCooldown;

    float _lastAttackTime;
    private void OnCollisionStay2D(Collision2D collision)
    {

        // Abort if we already attacked recently.
        if (Time.time - _lastAttackTime < attackCooldown) return;
        var pc = collision.gameObject.GetComponent<PlayerHealth>();
        if (pc)
        {
            pc.TakeDamage(damage);
            // Remember that we recently attacked.
            _lastAttackTime = Time.time;
        }

    }
}
