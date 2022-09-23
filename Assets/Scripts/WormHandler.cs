using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormHandler : MonoBehaviour, ILife
{
    [SerializeField] private Material[] _wormMaterials;

    private WormMovement _wormMovement;
    private Camera _wormCamera;
    private AudioListener _audioListener;
    private TurnHandler _turnHandler;
    private Renderer _renderer;
    private WormWeaponHandler _wormWeaponHandler;

    private int _controllingPlayer;
    private bool _isActive;

    private int _maxLife = 100;
    private int _curLife;

    void Start()
    {
        Init();
    }

    

    private void Init()
    {
        _curLife = _maxLife;
        _wormMovement = GetComponent<WormMovement>();
        _wormCamera = GetComponentInChildren<Camera>();
        _audioListener = GetComponentInChildren<AudioListener>();
        _isActive = false;
        _turnHandler = TurnHandler.Instance;
        _renderer = GetComponent<Renderer>();
        _wormWeaponHandler = GetComponentInChildren<WormWeaponHandler>();
    }

    public void StartTurn()
    {
        _isActive = true;
        _wormCamera.depth = 10;
        _audioListener.enabled = true;
        _wormWeaponHandler.EquipWeapon(0);
        _wormWeaponHandler.ResetShotStatus();
    }

    public void EndTurn()
    {
        _wormMovement.Stop();
        _isActive = false;
        _wormCamera.depth = 0;
        _audioListener.enabled = false;
        _turnHandler.AddWorm(gameObject);
        _wormWeaponHandler.UnEquipWeapon();
    }

    public void EndTurn(float delay)
    {
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

        _renderer.material = _wormMaterials[Math.Clamp(_controllingPlayer, 0, _wormMaterials.Length)];
        
    }

    public void TakeDamage(int dmg)
    {
        _curLife -= dmg;
        if (_curLife <= 0)
        {
            _curLife = 0;
            Death();
        }
    }

    private void Death()
    {
        PlayerCounter.Instance.WormKilled(_controllingPlayer);
        _wormCamera.GetComponent<CameraDestructionDelay>().DelayDestruction();
        Destroy(gameObject);
    }

    public Camera GetCamera()
    {
        return _wormCamera;
    }

    public float GetLifeRatio()
    {
        return (float) _curLife / _maxLife;
    }
}
