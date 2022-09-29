using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class WormHandler : MonoBehaviour, ILife
{
    [SerializeField] private Material[] _wormMaterials;

    public Camera WormCamera { get; private set; }
    public CinemachineVirtualCamera WormCinemachine { get; private set; }

    private AudioListener _audioListener;
    private TurnHandler _turnHandler;
    private Renderer _renderer;
    private WormWeaponHandler _wormWeaponHandler;
    private int _controllingPlayer;
    private PlayerInputController _playerInputController;
    private WormMovement _wormMovement;

    private bool _isActive;

    private float _turnDelay = 2.5f;

    private int _maxLife = 100;
    private int _curLife;

    void Start()
    {
        Init();
    }

    

    private void Init()
    {
        _curLife = _maxLife;
        WormCamera = GetComponentInChildren<Camera>();
        WormCinemachine = GetComponentInChildren<CinemachineVirtualCamera>();
        _audioListener = GetComponentInChildren<AudioListener>();
        _isActive = false;
        _turnHandler = TurnHandler.Instance;
        _renderer = GetComponent<Renderer>();
        _wormWeaponHandler = GetComponentInChildren<WormWeaponHandler>();
        _wormMovement = GetComponent<WormMovement>();
        _playerInputController = PlayerInputController.Instance;
    }

    /*public void StartTurn()
    {
        _isActive = true;
        WormCinemachine.Priority = 2;
        _wormWeaponHandler.EquipWeapon(0);
        _wormWeaponHandler.ResetShotStatus();
        //WormCamera.depth = 2;
        //WormCinemachine.Priority = 2;

        _playerInputController.SetCurrentWorm(_wormMovement, _wormWeaponHandler);
    }*/

    private IEnumerator IEStartTurn(float delay)
    {
        yield return new WaitForSeconds(_turnDelay);
        _wormWeaponHandler.EquipWeapon(0);
        _isActive = true;
        _wormWeaponHandler.ResetShotStatus();
        _playerInputController.SetCurrentWorm(_wormMovement, _wormWeaponHandler);
    }
    public void StartTurn()
    {
        WormCinemachine.Priority = 2;
        _audioListener.enabled = true;
        StartCoroutine(IEStartTurn(_turnDelay));

        //_audioListener.enabled = true;
        //Invoke("StartTurn", delay);
    }
    public void EndTurn()
    {
        StartCoroutine(IEEndTurn(_turnDelay));
        
    }

    private IEnumerator IEEndTurn(float delay)
    {
        yield return new WaitForSeconds(delay); 
        _isActive = false;
        _audioListener.enabled = false;
        _wormWeaponHandler.UnEquipWeapon();
        //WormCamera.depth = 0;
        //WormCinemachine.Priority = 0;
        WormCinemachine.Priority = 0;
        _playerInputController.SetCurrentWorm(null, null);
        _turnHandler.NextActiveWorm();
    }

    public void EndTurn(float delay)
    {
        _turnHandler.AddWorm(gameObject);
        Invoke("EndTurn", delay);
    }

    

    public bool IsActive()
    {
        return _isActive;
    }

    public void SetControllingPlayer(int playerID)
    {
        _controllingPlayer = playerID;
        ChangeColor();
    }

    public int GetControllingPlayer()
    {
        return _controllingPlayer;
    }

    private void ChangeColor()
    {
        if (_renderer == null)
        {
            _renderer = GetComponent<Renderer>();
        }

        _renderer.material.SetColor("_Color", PlayerColors.ToColor(_controllingPlayer));
        
    }

    public void TakeDamage(int dmg)
    {
        if (_curLife <= 0) //Prevents multiple projectiles killing the unit in the same frame.
        {
            return;
        }

        _curLife -= dmg;
        if (_curLife <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        PlayerCounter.Instance.WormKilled(_controllingPlayer);
        WormCamera.GetComponent<CameraDestructionDelay>().DelayDestruction();
        WormCinemachine.GetComponent<CameraDestructionDelay>().DelayDestruction();
        _playerInputController.SetCurrentWorm(null, null);
        WormCinemachine.Priority = 0;
        _audioListener.enabled = false;
        if (_turnHandler.GetActiveWorm() == gameObject)
            _turnHandler.NextActiveWorm(_turnDelay);
        Destroy(gameObject);
    }

    public float GetLifeRatio()
    {
        return (float) _curLife / _maxLife;
    }

}
