using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public static PlayerCounter Instance { get; private set; }

    private int _playerCount;
    private int[] _characterCount;


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

    public void Setup(int playerCount ,int wormCount)
    {
        _playerCount = playerCount;
        _characterCount = new int[_playerCount];
        for (int i = 0; i < _characterCount.Length; i++)
        {
            _characterCount[i] = wormCount;
        }
        UpdateText();
    }

    public void UpdateText()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Worms remaining: \n");
        for (int i = 0; i < _playerCount; i++)
        {
            stringBuilder.Append("Player " + i + ": " + _characterCount[i] + "\n");
        }

        _text.text = stringBuilder.ToString();
    }

    public void WormKilled(int playerID)
    {
        _characterCount[playerID]--;
        UpdateText();
    }

}
