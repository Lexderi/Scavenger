using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyController : EnemyController
{
    protected override float MaxHealth => 100;

    public override void OnDeath()
    {
        print("DEADDDDDDDDDDDDDDD!");
        Destroy(gameObject);
    }
}
