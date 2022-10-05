using UnityEngine;

public class ChargeMeter : MonoBehaviour
{
    [SerializeField] private GameObject chargeBar;

    
    public void Start()
    {
        SetActive(false);
    }

    public void UpdateBar(float percentage)
    {
        chargeBar.transform.localScale = new Vector3(percentage, 1.01f, 1.01f);
        chargeBar.transform.localPosition = new Vector3(percentage / 2 - 0.5f, 0, 0);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

}
