using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunSlug : MonoBehaviour
{
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        
        if (otherCollider.GetComponent<ShotGunSlug>() != null)
        {
            return;
        }
        var hit = otherCollider.GetComponent<ILife>();
        if (hit != null)
        {
            hit.TakeDamage(1);
            otherCollider.GetComponent<Rigidbody>().AddForce(_rb.velocity*0.01f, ForceMode.Impulse);
        }

        Debug.Log("hit");
        Destroy(gameObject);
    }
}
