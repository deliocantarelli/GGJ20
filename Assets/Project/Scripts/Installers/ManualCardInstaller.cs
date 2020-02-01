using GGJ20.CardRules;
using UnityEngine;
using Zenject;

namespace GGJ20.Installers
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