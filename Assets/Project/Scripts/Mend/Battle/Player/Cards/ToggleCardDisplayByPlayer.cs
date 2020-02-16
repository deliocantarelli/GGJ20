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
    [RequireComponent(typeof(CardDisplay))]
    class ToggleCardDisplayByPlayer : MonoBehaviour
    {
        private CardDisplay card;
        private CanvasGroup cg;
        private Player player;

        [Inject]
        private void Init(Player player)
        {
            cg = GetComponent<CanvasGroup>();
            card = GetComponent<CardDisplay>();
            this.player = player;
            player.UsableManaChanged += CheckPlayable;
        }

        private void Start()
        {
            CheckPlayable(player);
        }
        private void OnDestroy()
        {
            player.UsableManaChanged -= CheckPlayable;
        }


        private void CheckPlayable(Player player)
        {
            cg.interactable = player.CanPlayCard(card.Card);
        }
    }
}
