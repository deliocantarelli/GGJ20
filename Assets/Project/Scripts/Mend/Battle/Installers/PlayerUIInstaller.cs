using PointNSheep.Mend.Meta;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PointNSheep.Mend.Battle
{
    public class PlayerUIInstaller : MonoInstaller
    {
        [SerializeField]
        private Player.Settings playerSettings;
        [SerializeField]
        private SpellAimController spellAimPrefab;

        public override void InstallBindings()
        {
            Container.Bind<Player.Settings>().FromInstance(playerSettings).AsSingle();

            Container.Bind<SpellAimController>().FromComponentInNewPrefab(spellAimPrefab)
                .UnderTransformGroup("UI")
                .AsSingle();

            Container.Bind<Player>().FromNewComponentOnNewGameObject()
                .WithGameObjectName("Player")
                .UnderTransformGroup("Logic").AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<PlayerController>().FromNewComponentOnNewGameObject()
                .WithGameObjectName("Player Hand")
                .UnderTransformGroup("Logic").AsSingle().NonLazy();

            Container.Bind<Deck>().AsSingle();

            Container.Bind<BattleSceneController>()
                .FromNewComponentOnNewGameObject()
                .WithGameObjectName("Scene Controller")
                .UnderTransformGroup("Logic")
                .AsSingle();

            Container.Bind<IEnumerable<Card>>()
                .FromResolveGetter<GameStateController>(c => c.CurrentRun.CardsInDeck)
                .WhenInjectedInto<Deck>();

            //Container.Bind<Card>().FromResolveGetter<Deck>(d => d.Draw())
            //    .AsTransient()
            //    .WhenInjectedInto<CardDisplay>();
        }
    }
}