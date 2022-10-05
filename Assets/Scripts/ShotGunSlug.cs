using UnityEngine;

public class ShotGunSlug : MonoBehaviour
{
    private Rigidbody _rb;
    private float _spawnedAtTime;
    private float _minDuration = .5f;
    private float _maxDuration = 1f;
    private float _duration;

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        ResetTimer();
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void ResetTimer()
    {
        _spawnedAtTime = Time.time;
        _duration = Random.Range(_minDuration, _maxDuration);
    }

    private void Update()
    {
        if (Time.time > _spawnedAtTime + _duration)
        {
            Recycle();
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

        Recycle();
    }

    private void Recycle()
    {
        _rb.velocity = Vector3.zero;
        ObjectStorage.Instance.ReturnShotGunSlug(gameObject);
    }

}
