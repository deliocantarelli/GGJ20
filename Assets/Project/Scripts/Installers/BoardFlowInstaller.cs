using UnityEngine;
using Zenject;

namespace GGJ20.Battery {
    public class BoardFlowInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform LogicObjectParent;
        public override void InstallBindings()
        {
            Container.Bind<BatteryManager>().FromNewComponentOnNewGameObject().UnderTransform(LogicObjectParent).AsSingle().NonLazy();
            Container.Bind<GameBatteries>().FromNew().AsSingle().NonLazy();
        }
    }
}