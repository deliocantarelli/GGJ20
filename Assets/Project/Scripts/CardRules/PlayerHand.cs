using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace GGJ20.CardRules
{
    public class PlayerHand : MonoBehaviour
    {
        private List<CardDisplay> cardDisplays;
        private Player player;
        private Deck deck;

        [Inject]
        private void Init(Player player, List<CardDisplay> cardDisplays, Deck deck)
        {
            this.player = player;
            this.cardDisplays = cardDisplays;
            this.deck = deck;
        }
    }
}
