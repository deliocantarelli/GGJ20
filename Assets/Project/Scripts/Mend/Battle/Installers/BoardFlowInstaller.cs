using UnityEngine;
using Zenject;

namespace PointNSheep.Mend.Battle {
    public class BoardFlowInstaller : MonoInstaller
    {
        [SerializeField]
        private LevelSettings levelSettings;
        public override void InstallBindings()
        {
            Container.Bind<TargetsManager>().FromNewComponentOnNewGameObject()
                .UnderTransformGroup("Logic")
                .AsSingle().NonLazy();
            Container.Bind<GameTargets>().FromNew().AsSingle().NonLazy();
            Container.Bind<BatteryManager>().FromNewComponentOnNewGameObject()
                .UnderTransformGroup("Logic")
                .AsSingle().NonLazy();
            Container.Bind<LevelSettings>().FromInstance(levelSettings)
                .AsSingle();
        }
    }
}