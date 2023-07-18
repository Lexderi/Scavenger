using MyBox;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthBarController : MonoBehaviour
{
    // inspector settings
    [SerializeField] [InitializationField] private bool trackPlayer;

    // references
    private EnemyController enemyController;
    [Inject] private PlayerController player;
    private Slider healthBar;

    private void Awake()
    {
        // get components
        if (!trackPlayer)
            enemyController = GetComponentInParent<EnemyController>();
        healthBar = GetComponent<Slider>();

        // init health bar variables
        if (trackPlayer)
        {
            healthBar.maxValue = player.MaxHealth;
            healthBar.value = player.MaxHealth;
        }
        else
        {
            healthBar.maxValue = enemyController.MaxHealth;
            healthBar.value = enemyController.MaxHealth;
        }
    }

    public void UpdateHealthBar() => healthBar.value = trackPlayer ? player.Health : enemyController.Health;
}