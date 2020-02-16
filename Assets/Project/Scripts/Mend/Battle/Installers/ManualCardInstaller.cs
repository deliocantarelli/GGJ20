using UnityEngine;
using Zenject;

namespace PointNSheep.Mend.Battle
{
    public class ManualCardInstaller : MonoInstaller
    {
        [SerializeField]
        private Card card;
        public override void InstallBindings()
        {
            Container.Bind<Card>().FromInstance(card).AsSingle();
        }
    }
}