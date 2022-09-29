using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class WeaponAnimation : MonoBehaviour
{
    private Animation _animation;
    [SerializeField] private AnimationClip _equipAnimation;
    [SerializeField] private AnimationClip _unEquipAnimation;

    private void Awake()
    {
        _animation = GetComponent<Animation>();
    }

    public void PlayEquip()
    {
        _animation.Play(_equipAnimation.name);
    }
    public void PlayUnEquip()
    {
        _animation.Play(_unEquipAnimation.name);
    }
}
