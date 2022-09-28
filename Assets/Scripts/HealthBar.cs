using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    
    private TurnHandler _turnhandler;
    private Camera _camera;
    private WormHandler _wormHandler;

    

    void Start()
    {
        Init();
    }

    void Update()
    {
        UpdateCamera();
        UpdateLife();
    }

    private void Init()
    {
        _wormHandler = GetComponentInParent<WormHandler>();
        if (_wormHandler == null)
        {
            Debug.Log("failed to get worm");
        }
    }

    private void UpdateCamera()
    {
        if (_turnhandler == null)
        {
            _turnhandler = TurnHandler.Instance;
        }

        GameObject currentWorm = _turnhandler.GetActiveWorm();
        if (currentWorm != null)
        {
            _camera = currentWorm.GetComponent<WormHandler>().WormCamera;
        }
        
        if (_camera == null)
        {
            _slider.gameObject.SetActive(false);
            return;
        }
        _slider.gameObject.SetActive(true);
        _slider.transform.LookAt(_camera.transform);
    }

    private void UpdateLife()
    {
        _slider.value = _wormHandler.GetLifeRatio();
    }
}
