using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunSlug : MonoBehaviour
{
    private Rigidbody _rb;
    private float _spawnedAtTime;
    private float _minDuration = .5f;
    private float _maxDuration = 1f;
    private float _duration;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody>();
        _spawnedAtTime = Time.time;
        _duration = Random.Range(_minDuration, _maxDuration);
    }

    private void Update()
    {
        if (Time.time > _spawnedAtTime + _duration)
        {
            Destroy(gameObject);
        }
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
