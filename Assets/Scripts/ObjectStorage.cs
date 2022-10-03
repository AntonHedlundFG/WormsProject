using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ObjectStorage : MonoBehaviour
{
    [SerializeField] private GameObject _slugGameObject;
    public static ObjectStorage Instance { private set; get; }
    private Queue<GameObject> _shotGunSlugs;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        Init();
    }

    private void Init()
    {
        _shotGunSlugs = new Queue<GameObject>();
    }

    public GameObject GetShotGunSlug()
    {
        GameObject returnSlug;
        if (_shotGunSlugs.Count > 0)
        {
            returnSlug = _shotGunSlugs.Dequeue();
            returnSlug.SetActive(true);
        }
            
        else
            returnSlug = Instantiate(_slugGameObject, transform);
        return returnSlug;
    }

    public void ReturnShotGunSlug(GameObject returnSlug)
    {
        returnSlug.SetActive(false);
        _shotGunSlugs.Enqueue(returnSlug);
    }

    public void ClearAllStorage()
    {
        _shotGunSlugs.Clear();
    }
}
