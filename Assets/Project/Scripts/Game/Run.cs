﻿using GGJ20.CardRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace GGJ20.Game
{
    public class Run
    {
        [Serializable]
        public class Configs
        {
            [SerializeField]
            public List<Card> starterDeck;

            [SerializeField]
            public List<Card> cardPool;
        }

        [Inject]
        private Configs configs;


        private List<Card> deck;
        public int Floor { get; set; }

        public IEnumerable<Card> CardsInDeck { get { return deck.AsEnumerable(); } }

        public IEnumerable<Card> GenerateBooster(int size)
        {
            return configs.cardPool.OrderBy(c => UnityEngine.Random.value).Take(size);
        }

        public void Replace(Card take, Card place)
        {
            deck.RemoveAt(deck.IndexOf(take));
            deck.Add(place);
        }

        [Inject]
        private void Init()
        {
            this.deck = new List<Card>(configs.starterDeck);
        }

        public class Factory : PlaceholderFactory<Run>
        {

        }
    }
}
