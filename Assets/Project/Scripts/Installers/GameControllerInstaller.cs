using GGJ20.Audio;
using GGJ20.Game;
using UnityEngine;
using Zenject;

namespace GGJ20.Installers {
    public class GameControllerInstaller : MonoInstaller
    {
        public AudioManager audioManagerPrefab;
        public override void InstallBindings()
        {
            Container.Bind<GameStateController>()
                .FromNewComponentOnNewGameObject()
                .UnderTransform(transform)
                .AsSingle();
            Container.BindFactory<Run, Run.Factory>().AsSingle();

            Container.Bind<AudioManager>()
                .FromComponentInNewPrefab(audioManagerPrefab)
                .AsSingle().NonLazy();
        }
    }
}