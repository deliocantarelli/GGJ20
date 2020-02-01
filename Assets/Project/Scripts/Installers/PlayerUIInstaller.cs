using GGJ20.CardRules;
using UnityEngine;
using Zenject;

namespace GGJ20.Installers
{
    public class PlayerUIInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform LogicObjectsParent;
        [SerializeField]
        private Player.Settings playerSettings;

        public override void InstallBindings()
        {
            Container.Bind<Player.Settings>().FromInstance(playerSettings).AsSingle();

            Container.Bind<Player>().FromNewComponentOnNewGameObject()
                .UnderTransform(LogicObjectsParent).AsSingle().NonLazy();
        }
    }
}