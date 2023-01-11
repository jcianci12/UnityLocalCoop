using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toxicwaste : MonoBehaviour
{
    public int damage;
    public float attackCooldown;

    float _lastAttackTime;
    //private void OnCollisionStay(Collision collision)
    //{

    //    // Abort if we already attacked recently.
    //    if (Time.time - _lastAttackTime < attackCooldown) return;
    //    var pc = collision.gameObject.GetComponentInParent<PlayerHealth>();
    //    if (pc)
    //    {
    //        pc.TakeDamage(damage);
    //        // Remember that we recently attacked.
    //        _lastAttackTime = Time.time;
    //    }

    //}
    private void OnTriggerStay(Collider collision)
    {
        //Abort if we already attacked recently.
        if (Time.time - _lastAttackTime < attackCooldown) return;
        var pc = collision.gameObject.GetComponentInParent<PlayerHealth>();
        if (pc)
        {
            Debug.Log("damage" + pc.slider.value);
            pc.TakeDamage(damage);
            // Remember that we recently attacked.
            _lastAttackTime = Time.time;
        }
        // Debug-draw all contact points and normals
        //foreach (ContactPoint contact in collision)
        //{
        //    Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
        //}
    }
    //void OnCollisionEnter(Collision collisionInfo)
    //{

    //    //Abort if we already attacked recently.
    //        if (Time.time - _lastAttackTime < attackCooldown) return;
    //    var pc = collisionInfo.gameObject.GetComponentInParent<PlayerHealth>();
    //    if (pc)
    //    {
    //        pc.TakeDamage(damage);
    //        // Remember that we recently attacked.
    //        _lastAttackTime = Time.time;
    //    }
    //    // Debug-draw all contact points and normals
    //    foreach (ContactPoint contact in collisionInfo.contacts)
    //    {
    //        Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
    //    }
    //}
}
