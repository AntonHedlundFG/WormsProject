using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ObjectStorage : MonoBehaviour
{
    [SerializeField] private GameObject _slugGameObject;
    public static ObjectStorage Instance { set; private get; }
    private Queue<GameObject> _slugs;

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
        _slugs = new Queue<GameObject>();
    }

    public GameObject GetShotGunSlug()
    {
        GameObject returnSlug;
        if (_slugs.Count > 0)
            returnSlug = _slugs.Dequeue();
        else
            returnSlug = Instantiate(_slugGameObject);
        return returnSlug;
    }

    public void ReturnShotGunSlug(GameObject returnSlug)
    {
        returnSlug.SetActive(false);
        _slugs.Enqueue(returnSlug);
    }

    public void ClearAllStorage()
    {
        _slugs.Clear();
    }
}
