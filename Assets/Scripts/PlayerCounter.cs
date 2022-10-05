using UnityEngine;
using TMPro;

public class PlayerCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerText;
    [SerializeField] private TMP_Text[] _wormTexts;

    public static PlayerCounter Instance { get; private set; }

    private int _playerCount;
    private int[] _wormCount;


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
        _wormCount = new int[_playerCount];
        for (int i = 0; i < _wormCount.Length; i++)
        {
            _wormCount[i] = wormCount;
        }
        UpdateText();
    }

    public void UpdateText()
    {
        _playerText.text = "Worms remaining: ";
        for (int i = 0; i < _playerCount; i++)
        {
            _wormTexts[i].text = PlayerColors.ToString(i) + " worms: " + _wormCount[i];
            _wormTexts[i].color = PlayerColors.ToColor(i);
        }
    }

    public void WormKilled(int playerID)
    {
        _wormCount[playerID] = _wormCount[playerID] - 1;
        UpdateText();
    }

    public void EndGame()
    {
        _playerText.text = "";
        foreach (TMP_Text text in _wormTexts)
        {
            text.text = "";
        }
    }

}
