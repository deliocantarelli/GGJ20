using UnityEngine;
using Zenject;

namespace PointNSheep.Pathfinding
{
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