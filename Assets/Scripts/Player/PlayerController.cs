using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour, IDamageable
{
    // settings
    public float MaxHealth = 100;

    // references
    [Inject] private HealthBarController healthBar;

    // vars
    [HideInInspector] public float Health;


    private void Awake() =>
        // init variables
        Health = MaxHealth;

    public void Damage(float damage)
    {
        Health -= damage;

        healthBar.UpdateHealthBar();

        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die() => print("player died");
    // TODO: proper death
}