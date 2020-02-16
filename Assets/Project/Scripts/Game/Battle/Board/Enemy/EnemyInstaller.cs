using GGJ20.Enemy;
using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        Container.BindFactory<UnityEngine.Object, EnemyController, EnemyController.Factory>().FromFactory<PrefabFactory<EnemyController>>();
    }
}