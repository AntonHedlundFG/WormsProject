using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private GameObject _explosionGameObject;

    private float _createdTime;
    private float _timeBeforeActive = .2f;
    
    void Start()
    {
        _createdTime = Time.time;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (Time.time >= _createdTime + _timeBeforeActive)
        {
            CreateExplosion();
        }
    }

    private void CreateExplosion()
    {
        var explosion = Instantiate(_explosionGameObject, transform.position, Quaternion.identity);
        Explosion explosionScript = explosion.GetComponent<Explosion>();
        explosionScript.Init(transform.position);
        Destroy(gameObject);
    }
    
}
