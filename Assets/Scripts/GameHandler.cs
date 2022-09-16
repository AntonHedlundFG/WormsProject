using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private GameObject wormGameObject;
    [SerializeField] private GameObject turnHandlerGameObject;
    
    private TurnHandler _turnHandler;

    public static GameHandler Instance { get; private set; }

    private bool TESTstartSpawned = false;
    
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
    
    void Update()
    {
        if (!TESTstartSpawned)
        {
            CreateNewWorm(0);
            CreateNewWorm(1);
            TESTstartSpawned = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            _turnHandler.NextActiveWorm();
        }
    }

    private void Init()
    {

        if (TurnHandler.Instance == null)
        {
            Instantiate(turnHandlerGameObject);
        }
        _turnHandler = TurnHandler.Instance;

    }

    private void CreateNewWorm(int playerID)
    {
        GameObject newWorm = Instantiate(wormGameObject, new Vector3(Random.Range(30, 70), 7, 120), Quaternion.identity);
        _turnHandler.AddWorm(newWorm);
        newWorm.GetComponent<WormHandler>().SetControllingPlayer(playerID);
    }

    
}
