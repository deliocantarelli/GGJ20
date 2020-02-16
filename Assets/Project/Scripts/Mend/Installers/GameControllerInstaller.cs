using PointNSheep.Common.Audio;
using PointNSheep.Mend.Meta;
using UnityEngine;
using Zenject;

namespace PointNSheep.Mend {
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