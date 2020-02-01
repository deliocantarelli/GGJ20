using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace GGJ20.CardRules
{
    public class PlayerHandController : MonoBehaviour
    {
        private Player player;
        private Deck deck;


        [Inject]
        private void Init(Player player, Deck deck)
        {
            this.player = player;
            this.deck = deck;
        }


        public void OnSelected(CardDisplay cardDisplay)
        {
            if (player.TryPlayCard(cardDisplay.Card))
            {
                OnCardUsed(cardDisplay);
            }
        }
        public void OnCardUsed(CardDisplay cardDisplay)
        {
            StartCoroutine(CardUseAndRestock(cardDisplay));
        }

        private IEnumerator CardUseAndRestock(CardDisplay cardDisplay)
        {
            yield return cardDisplay.UsageAnimationCoroutine();

            deck.PutCardOnBottom(cardDisplay.Card);
            cardDisplay.SetCard(deck.Draw());

            yield return cardDisplay.NewCardCoroutine();
        }
    }
}
