using GGJ20.CardRules;
using GGJ20.Common;
using GGJ20.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace GGJ20.World
{
    public class Spell : MonoBehaviour
    {
        [Serializable]
        public class Description
        {
            public List<Hit> hits;
        }
        [Serializable]
        public class Hit
        {
            public enum Type { Damage, Wall }

            public Type type = Type.Damage;
            public float time;
            public int damage = 1;
            public int repeats;
            public float repeatInterval = 1;
            public float wallDuration = 1;
            public List<Vector2Int> Locations { get { return tileData.GetLocations(new Vector2Int(3,3)); } }

            public TileData tileData;

            public Hit()
            {

            }
            public Hit(float time, int damage, int repeats, float repeatInterval, TileData tileData)
            {
                this.time = time;
                this.damage = damage;
                this.repeats = repeats;
                this.repeatInterval = repeatInterval;
                this.tileData = tileData;
            }

            public Hit WithTime(float t)
            {
                return new Hit(t, damage, 0, 0, tileData);
            }
        }

        [Inject]
        private HitSpellElement.Pool spellElsPool;
        [Inject]
        private WallSpellElement.Pool spellWallElsPool;

        private Vector2Int epicenter;
        public Card Card { get; private set; }
        private List<Hit> nextHits;

        private Stopwatch stopwatch;


        [Inject]
        private void Init(Card card, WorldGrid grid, Vector2Int epicenter)
        {
            transform.position = grid.GridToWorld(epicenter, WorldGrid.PlaceMode.TileCenter);
            this.epicenter = epicenter;
            this.Card = card;

            stopwatch = Stopwatch.CreateAndStart();
            InitHits();
            CheckSpawns();
        }

        private void InitHits()
        {
            nextHits = Card.Spell.hits.ToList();
            var hitsToAdd = new List<Hit>();
            foreach (var hit in nextHits)
            {
                float t = hit.time;
                for (int i = 0; i < hit.repeats; i++)
                {
                    t += hit.repeatInterval;
                    hitsToAdd.Add(hit.WithTime(t));
                }
            }
            nextHits.AddRange(hitsToAdd);
        }

        private void Update()
        {
            CheckSpawns();
        }

        private void CheckSpawns()
        {
            var spawnNow = nextHits.Where(h => h.time <= stopwatch.ElapsedSeconds).ToArray();

            foreach (var hit in spawnNow)
            {
                foreach (var pos in hit.Locations)
                {
                    switch (hit.type)
                    {
                        case Hit.Type.Damage:
                            spellElsPool.Spawn(this, hit, pos + epicenter);
                            break;
                        case Hit.Type.Wall:
                            spellWallElsPool.Spawn(this, hit, pos + epicenter);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
                nextHits.Remove(hit);
            }
            if(nextHits.Count == 0)
            {
                OnFinshed();
            }
        }

        private void OnFinshed()
        {
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<Card, Vector2Int, Spell>
        {

        }
    }
}