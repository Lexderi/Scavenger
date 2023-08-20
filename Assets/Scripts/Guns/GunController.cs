using System.Collections;
using System.Linq;
using LuLib.Vector;
using MyBox;
using UnityEngine;

public abstract class GunController : MonoBehaviour, IEquipable
{
    // specs
    protected abstract float Damage { get; }
    protected abstract float FireCooldown { get; }
    protected abstract float Spread { get; }
    protected abstract uint MagazineSize { get; }
    protected abstract float ReloadTime { get; }
    public abstract float MovementSpeedPenalty { get; }
    protected abstract float Handling { get; }
    InventoryItemData IItem.InventoryData => inventoryData;
    
    protected abstract ParticleSystem ParticleSystem { get; }
    [Separator("References")]
    [SerializeField] private InventoryItemData inventoryData;

    // variables
    private float cooldownProgress;
    private uint magazineCount;
    private static int enemyLayerMask;

    public string HudInformation => $"Ammo: {magazineCount} / {MagazineSize}";

    private void Awake()
    {
        magazineCount = MagazineSize;
        cooldownProgress = FireCooldown;

        enemyLayerMask = LayerMask.GetMask("Enemy", "Wall");
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
        float spread = Random.Range(-Spread, Spread);
        direction.Rotate(0, spread, 0); // add spread

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
            rotation = spread
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

    public GameObject GetGameObject() => gameObject;
}