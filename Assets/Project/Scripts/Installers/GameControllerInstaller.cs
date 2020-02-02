using GGJ20.Game;
using UnityEngine;
using Zenject;

namespace GGJ20.Installers {
    public class GameControllerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameStateController>()
                .FromNewComponentOnNewGameObject()
                .UnderTransform(transform)
                .AsSingle();
        }
    }
}