using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class ContainerWeapons : MonoBehaviour
{
    public List<WeaponController> weapons;
    public static ContainerWeapons instance;
    private void Awake()
    {
        instance = this;
    }
}
