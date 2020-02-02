using GGJ20.CardRules;
using GGJ20.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GGJ20.Installers
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