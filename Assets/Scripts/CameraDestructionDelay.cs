using UnityEngine;

public class CameraDestructionDelay : MonoBehaviour
{

    public void DelayDestruction()
    {
        transform.SetParent(null);
        Destroy(gameObject, 5f);
    }

}
