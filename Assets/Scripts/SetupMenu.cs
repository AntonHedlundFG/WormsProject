using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetupMenu : MonoBehaviour
{
    [SerializeField] private Slider _playerCountSlider;
    [SerializeField] private Slider _wormCountSlider;
    [SerializeField] private Button _startButton;
    [SerializeField] private TMP_Text _winnerText;
    [SerializeField] private TMP_Text _playerCountText;
    [SerializeField] private TMP_Text _wormCountText;

    public static SetupMenu Instance { get; private set; }

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

    public void StartGame()
    {
        _playerCountSlider.gameObject.SetActive(false);
        _wormCountSlider.gameObject.SetActive(false);
        _startButton.gameObject.SetActive(false);
        _playerCountText.gameObject.SetActive(false);
        _wormCountText.gameObject.SetActive(false);
        _winnerText.text = "";
        GameHandler.Instance.Setup((int)_playerCountSlider.value, (int)_wormCountSlider.value);
    }

    public void EndGame(int winningPlayer)
    {
        _playerCountSlider.gameObject.SetActive(true);
        _wormCountSlider.gameObject.SetActive(true);
        _startButton.gameObject.SetActive(true);
        _playerCountText.gameObject.SetActive(true);
        _wormCountText.gameObject.SetActive(true);
        _winnerText.text = "The winner is player " + winningPlayer + "!";
    }

    public void SliderChanges()
    {
        _playerCountText.text = "Players: " + _playerCountSlider.value;
        _wormCountText.text = "Worms: " + _wormCountSlider.value;
    }
}
