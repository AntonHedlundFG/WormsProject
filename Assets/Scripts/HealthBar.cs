using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    
    private TurnHandler _turnhandler;
    private CinemachineVirtualCamera _camera;
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
        _turnhandler = TurnHandler.Instance;
    }

    private void UpdateCamera()
    {

        GameObject currentWorm = _turnhandler.GetActiveWorm();
        if (currentWorm != null)
        {
            _camera = currentWorm.GetComponent<WormHandler>().WormCinemachine;
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
