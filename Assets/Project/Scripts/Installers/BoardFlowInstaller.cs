using UnityEngine;
using Zenject;
using GGJ20.Battery;
using GGJ20.Target;

namespace GGJ20.Installers {
    public class BoardFlowInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform LogicObjectParent;
        public override void InstallBindings()
        {
            Container.Bind<TargetsManager>().FromNewComponentOnNewGameObject()
                .UnderTransformGroup("Logic")
                .AsSingle().NonLazy();
            Container.Bind<GameTargets>().FromNew().AsSingle().NonLazy();
            Container.Bind<BatteryManager>().FromNewComponentOnNewGameObject()
                .UnderTransformGroup("Logic")
                .AsSingle().NonLazy();
        }
    }
}