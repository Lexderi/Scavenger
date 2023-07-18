using System.Collections;
using System.Linq;
using LuLib.Vector;
using UnityEngine;

public abstract class GunController : MonoBehaviour, IEquipable
{
    // specs
    protected abstract float Damage { get; }
    protected abstract float FireCooldown { get; }
    protected abstract float Accuracy { get; }
    protected abstract uint MagazineSize { get; }
    protected abstract float ReloadTime { get; }
    public abstract float MovementSpeedPenalty { get; }
    protected abstract float Handling { get; }

    // references
    protected abstract ParticleSystem ParticleSystem { get; }

    // variables
    private float cooldownProgress;
    private uint magazineCount;
    private static int enemyLayerMask;

    public string HudInformation => $"Ammo: {magazineCount} / {MagazineSize}";

    private void Awake()
    {
        magazineCount = MagazineSize;
        cooldownProgress = FireCooldown;

        enemyLayerMask = LayerMask.GetMask("Enemy");
    }

    private void Update() =>
        // update cooldownProgress
        cooldownProgress += Time.deltaTime;

    public void Shoot(int targetLayer)
    {
        // check if can shoot
        if (cooldownProgress < FireCooldown || magazineCount <= 0) return;

        // calculate shot direction
        Vector3 direction = transform.forward;
        float accuracyRotation = Random.Range(-Accuracy, Accuracy);
        direction.Rotate(0, accuracyRotation, 0); // add accuracy

        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, float.PositiveInfinity, targetLayer);

        // sort hits by distance
        hits = hits.OrderBy(hit => hit.distance).ToArray();

        // damage first enemy
        if (hits.Length > 0 && hits[0].transform.gameObject.layer != LayerMask.NameToLayer("Wall"))
        {
            IDamageable target = hits[0].transform.GetComponentInParent<IDamageable>();

            target.Damage(Damage);
        }

        // emit bullet particle
        ParticleSystem.EmitParams emitParams = new()
        {
            rotation = accuracyRotation
        };

        ParticleSystem.Emit(emitParams, 1);

        // update variables
        cooldownProgress = 0;
        magazineCount--;

        // reload if needed
        if (magazineCount <= 0) Reload();
    }

    protected void Reload() => StartCoroutine(ReloadCoroutine());

    private IEnumerator ReloadCoroutine()
    {
        // wait for reload time
        yield return new WaitForSeconds(ReloadTime);
        // actually reload
        magazineCount = MagazineSize;
    }

    public void Use() => Shoot(enemyLayerMask);
}