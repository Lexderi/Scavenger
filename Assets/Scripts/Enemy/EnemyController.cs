using System;
using System.Collections;
using System.Collections.Generic;
using LuLib.Transform;
using UnityEngine;
using Zenject;

public class EnemyController : MonoBehaviour
{
    [Inject] private static readonly PlayerController player;

    private void Start()
    {
        player.transform.SetPosY(10);
    }
}
