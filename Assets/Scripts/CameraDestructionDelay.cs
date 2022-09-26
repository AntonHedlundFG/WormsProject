using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDestructionDelay : MonoBehaviour
{

    public void DelayDestruction()
    {
        transform.SetParent(null);
        Destroy(gameObject, 1f);
    }

}
