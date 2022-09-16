using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private GameObject wormGameObject;

    private List<GameObject> _worms;
    private int _currentActiveWorm;


    // Start is called before the first frame update
    void Start()
    {
        Init();
        CreateNewWorm();
        CreateNewWorm();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            NextActiveWorm();
        }
    }

    private void Init()
    {
        _worms = new List<GameObject>();
        _currentActiveWorm = -1;

    }

    private void CreateNewWorm()
    {
        _worms.Add(Instantiate(wormGameObject, new Vector3(Random.Range(30,70), 7, 120), Quaternion.identity));
    }

    private void NextActiveWorm()
    {
        if (_worms.Count == 0) {return;}

        if (_currentActiveWorm == -1)
        {
            _currentActiveWorm++;
            _worms[_currentActiveWorm].GetComponent<WormMovement>().StartTurn();
            return;
        }

        _worms[_currentActiveWorm].GetComponent<WormMovement>().EndTurn();
        _currentActiveWorm = (_currentActiveWorm + 1) % _worms.Count;
        _worms[_currentActiveWorm].GetComponent<WormMovement>().StartTurn();

    }

}
