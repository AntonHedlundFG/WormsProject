using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private GameObject _wormGameObject;
    [SerializeField] private GameObject _turnHandlerGameObject;

    private int _playerAmount = 2;
    private int _wormsPerPlayer = 1;

    private TurnHandler _turnHandler;
    public static GameHandler Instance { get; private set; }

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

        if (TurnHandler.Instance == null)
        {
            Instantiate(_turnHandlerGameObject);
        }
        _turnHandler = TurnHandler.Instance;

    }

    public void Setup(int playerAmount, int wormsPerPlayer)
    {
        _playerAmount = playerAmount;
        _wormsPerPlayer = wormsPerPlayer;
        SetupWorms();
    }
    private void SetupWorms()
    {
        for (int i = 0; i < _wormsPerPlayer; i++)
        {
            for (int j = 0; j < _playerAmount; j++)
            {
                CreateNewWorm(j);
            }
        }

        _turnHandler.StartGame();
        PlayerCounter.Instance.Setup(_playerAmount, _wormsPerPlayer);
    }
    private void CreateNewWorm(int playerID)
    {
        GameObject newWorm = Instantiate(_wormGameObject, new Vector3(Random.Range(30, 70), 7, Random.Range(80, 120)), Quaternion.identity);
        _turnHandler.AddWorm(newWorm);
        newWorm.GetComponent<WormHandler>().SetControllingPlayer(playerID);
    }
}
