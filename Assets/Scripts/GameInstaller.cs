using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private PlayerController player;
    [SerializeField] private HealthBarController playerHealthBar;
    public override void InstallBindings()
    {
        Container.Bind<PlayerController>().FromInstance(player).AsSingle();
        Container.Bind<HealthBarController>().FromInstance(playerHealthBar).AsSingle();
    }
}