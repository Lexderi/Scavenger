using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    // references
    private EnemyController enemyController;
    private Slider healthBar;

    private void Awake()
    {
        // get components
        enemyController = GetComponentInParent<EnemyController>();
        healthBar = GetComponent<Slider>();

        // init health bar variables
        healthBar.maxValue = enemyController.MaxHealth;
        healthBar.value = enemyController.MaxHealth;
    }

    public void UpdateHealthBar()
    {
        healthBar.value = enemyController.Health;
    }
}
