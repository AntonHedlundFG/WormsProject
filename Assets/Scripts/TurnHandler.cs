using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    private Queue<GameObject> _worms;
    private GameHandler _gameHandler;

    private GameObject _currentActiveWorm;

    public static TurnHandler Instance { get; private set; }

    void Start()
    {
        Init();
    }
    
    void Update()
    {
        
    }
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

    private void Init()
    {
        _worms = new Queue<GameObject>();
        _gameHandler = GameHandler.Instance;
    }

    public void NextActiveWorm()
    {
        _currentActiveWorm?.GetComponent<WormHandler>().EndTurn();

        do
        {
            if (_worms.Count == 0)
            {
                return;
            }
            _currentActiveWorm = _worms.Dequeue();
        } while (_currentActiveWorm == null);

        _currentActiveWorm?.GetComponent<WormHandler>().StartTurn();
    }

    public void AddWorm(GameObject worm)
    {
        _worms.Enqueue(worm);
    }

    public void NextActiveWorm(float delay)
    {
        Invoke("NextActiveWorm", delay);
    }
    

}
