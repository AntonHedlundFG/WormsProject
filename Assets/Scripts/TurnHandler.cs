using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    private Queue<GameObject> _worms;
    private GameObject _currentActiveWorm;
    public static TurnHandler Instance { get; private set; }

    void Start()
    {
        Init();
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
    }

    public void NextActiveWorm()
    {
        if (_currentActiveWorm != null)
        {
            _worms.Enqueue(_currentActiveWorm);
        }
        CheckVictory();

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

    public GameObject GetActiveWorm()
    {
        return _currentActiveWorm;
    }

    public void StartGame()
    {
        NextActiveWorm(2f);
    }

    public void EndGame(int winningPlayer)
    {
        ClearBoard();
        PlayerCounter.Instance.EndGame();
        SetupMenu.Instance.EndGame(winningPlayer);
    }

    private void ClearBoard()
    {
        while (_worms.Count > 0)
        {
            GameObject worm = _worms.Dequeue();
            Destroy(worm);
        }
    }
    private void CheckVictory()
    {
        List<int> remainingPlayers = new List<int>();
        int loopAmount = _worms.Count;
        for (int i = 0; i < loopAmount; i++)
        {
            GameObject worm = _worms.Dequeue();
            if (worm != null)
            {
                WormHandler wormHandler = worm.GetComponent<WormHandler>();
                if (!remainingPlayers.Contains(wormHandler.GetControllingPlayer()))
                {
                    remainingPlayers.Add(wormHandler.GetControllingPlayer());
                }
                _worms.Enqueue(worm);
            }
        }

        if (remainingPlayers.Count == 1)
        {
            EndGame(remainingPlayers[0]);
        }

        if (remainingPlayers.Count == 0)
        {
            EndGame(-1);
        }
    }

}
