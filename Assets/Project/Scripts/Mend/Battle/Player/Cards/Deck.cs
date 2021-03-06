﻿using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace PointNSheep.Mend.Battle
{
    public class Deck
    {
        private List<Card> cards;

        [Inject]
        private void Init(IEnumerable<Card> cards)
        {
            this.cards = new List<Card>(cards);
            Shuffle();
        }

        public Card Peek()
        {
            return cards.Count > 0 ? null :cards[0];
        }
        public Card Draw()
        {
            Card result = cards[0];
            cards.RemoveAt(0);
            return result;
        }
        public void PutCardOnBottom(Card card)
        {
            cards.Insert(cards.Count, card);
        }
        public void Shuffle()
        {
            cards = cards.OrderBy(c => UnityEngine.Random.value).ToList();
        }
    }
}