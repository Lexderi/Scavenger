using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private PlayerController player;
    public override void InstallBindings() => Container.Bind<PlayerController>().FromInstance(player).AsSingle();
}