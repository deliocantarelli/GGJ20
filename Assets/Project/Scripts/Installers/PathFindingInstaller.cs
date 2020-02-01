using UnityEngine;
using Zenject;

namespace GGJ20.PathFinding {
    public class PathFindingInstaller : MonoInstaller
    {
        [SerializeField]
        private PathFindingManager manager;
        public override void InstallBindings()
        {
            Container.Bind<PathFindingManager>().FromInstance(manager).AsSingle().NonLazy();
        }
    }
}