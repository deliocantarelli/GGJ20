using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace PointNSheep.Mend.Battle
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CardHandPanel : MonoBehaviour
    {
        private BattleSceneController controller;
        private CanvasGroup cg;
        [Inject]
        private Deck deck;

        [Inject]
        private void Inject(BattleSceneController controller)
        {
            this.controller = controller;

            cg = GetComponent<CanvasGroup>();

            this.controller.BattleOver += OnBattleOver;

            foreach (var card in GetComponentsInChildren<CardDisplay>())
            {
                card.SetCard(deck.Draw());
            }
        }

        private void OnBattleOver(GameResult obj)
        {
            cg.interactable = false;
        }
    }
}
