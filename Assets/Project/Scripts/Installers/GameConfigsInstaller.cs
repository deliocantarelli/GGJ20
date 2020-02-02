using GGJ20.Game;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameConfigsInstaller", menuName = "GGJ20/GameConfigsInstaller")]
public class GameConfigsInstaller : ScriptableObjectInstaller<GameConfigsInstaller>
{

    [SerializeField]
    private Run.Configs runConfigs;
    [SerializeField]
    private GameStateController.Configs gameConfigs;
    public override void InstallBindings()
    {
        Container.Bind<Run.Configs>().FromInstance(runConfigs);
        Container.Bind<GameStateController.Configs>().FromInstance(gameConfigs);
    }

}