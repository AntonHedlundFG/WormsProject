using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float _radius = 5f;
    private int _mindamage = 10; 
    private int  _maxdamage = 70;

    private ParticleSystem _ps;
    


    public void Init(Vector3 origin, float radius, int mindamage, int maxdamage)
    {
        _radius = radius;
        _mindamage = mindamage;
        _maxdamage = maxdamage;
        Init(origin);
    }

    public void Init(Vector3 origin)
    {
        _ps = GetComponent<ParticleSystem>();
        transform.position = origin;
        Detonate();
    }

    private void Detonate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius);
        
        foreach (var hitCollider in hitColliders)
        {
            Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(_maxdamage*20, transform.position, _radius, 2.0f);
            }

            ILife life = hitCollider.GetComponent<ILife>();
            if (life != null)
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                float rangePercentage = 1f - (distance / _radius);
                int damageTaken = Mathf.FloorToInt(_mindamage + rangePercentage * (_maxdamage - _mindamage));
                life.TakeDamage(damageTaken);
            }

        }

        var main = _ps.main;
        main.startSpeed = _radius / _ps.main.startLifetime.constant;
        _ps.Play();
        Destroy(gameObject, _ps.main.duration + _ps.main.startLifetime.constant);
    }


}
