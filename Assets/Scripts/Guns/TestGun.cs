using UnityEngine;

public class TestGun : GunController
{
    protected override float Damage => 10;
    protected override float FireCooldown => 0.1f;
    protected override float Spread => 0;
    protected override uint MagazineSize => 10;
    protected override float ReloadTime => 1;
    public override float MovementSpeedPenalty => 1;
    protected override float Handling => 1;
    protected override ParticleSystem ParticleSystem => particleSys;

    [SerializeField] private ParticleSystem particleSys;
}