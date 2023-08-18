using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private PlayerController player;
    [SerializeField] private HealthBarController playerHealthBar;
    [SerializeField] private InventoryController inventoryController;

    public override void InstallBindings()
    {
        Container.Bind<PlayerController>().FromInstance(player).AsSingle();
        Container.Bind<HealthBarController>().FromInstance(playerHealthBar).AsSingle();
        Container.Bind<InventoryController>().FromInstance(inventoryController).AsSingle();

    }
}