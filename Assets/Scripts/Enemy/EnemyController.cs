using UnityEngine;

public abstract class EnemyController : MonoBehaviour, IDamageable
{
    // specs
    public abstract float MaxHealth { get; }
    public abstract string Name { get; }

    // vars
    public float Health;
    protected bool Dead;

    // references
    private HealthBarController healthBar;
    protected int PlayerLayerMask;

    protected void Awake()
    {
        // get components
        healthBar = GetComponentInChildren<HealthBarController>();

        // init variables
        Health = MaxHealth;
        PlayerLayerMask = LayerMask.GetMask("Player");
    }

    public void Damage(float damage)
    {
        // remove health
        Health -= damage;

        healthBar.UpdateHealthBar();

        // check if dead
        if (Health <= 0)
        {
            Dead = true;
            OnDeath();
        }
    }

    public abstract void OnDeath();
}