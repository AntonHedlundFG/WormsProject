using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private float _createdTime;
    private float _timeBeforeActive = 1f;
    
    void Start()
    {
        _createdTime = Time.time;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (Time.time >= _createdTime + _timeBeforeActive)
        {
            Destroy(gameObject);
        }
    }
    
}
