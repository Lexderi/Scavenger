using System;
using MyBox;
using Zenject;

public class TestEnemyController : EnemyController, IOnSeePlayer
{
    private GunController gun;

    protected new void Awake()
    {
        base.Awake();

        gun = GetComponentInChildren<GunController>();
    }

    public override float MaxHealth => 100;
    public override string Name => "Test Enemy";
    public override void OnDeath()
    {
        print("DEADDDDDDDDDDDDDDD!");
        Destroy(gameObject);
    }

    public void OnSeePlayer()
    {
        gun.Shoot(PlayerLayerMask);
        print("trying to shoot");
    }
}