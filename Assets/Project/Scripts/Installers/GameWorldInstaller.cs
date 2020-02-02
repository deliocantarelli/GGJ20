using GGJ20.World;
using UnityEngine;
using Zenject;

public class GameWorldInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<WorldGrid>().FromComponentInHierarchy().AsSingle();
    }
}