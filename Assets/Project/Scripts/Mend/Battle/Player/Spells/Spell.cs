﻿using PointNSheep.Common.Grid;
using PointNSheep.Common.TileInspector;
using PointNSheep.Common.Timers;
using PointNSheep.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace PointNSheep.Mend.Battle
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
            public enum Type { Damage, Wall, Indicator }

            public Type type = Type.Damage;
            public float time;
            public int damage = 1;
            public int repeats;
            public float repeatInterval = 1;
            public float wallDuration { get { return wallEnd - time; } }
            public float wallEnd = 5;
            public List<Vector2Int> Locations { get { return tileData.GetLocations(new Vector2Int(3,3)); } }

            public TileData tileData;

            public Hit()
            {

            }
            public Hit(Type type, float time, int damage, int repeats, float repeatInterval, float wallEnd, TileData tileData)
            {
                this.time = time;
                this.damage = damage;
                this.repeats = repeats;
                this.repeatInterval = repeatInterval;
                this.tileData = tileData;
                this.type = type;
            }

            public Hit WithTime(float t)
            {
                return new Hit(type, t, damage, 0, 0, wallEnd, tileData);
            }
        }

        [Inject]
        private HitSpellElement.Pool spellElsPool;
        [Inject]
        private WallSpellElement.Pool spellWallElsPool;


        public UnityEvent SpawnedHit;
        public UnityEvent SpawnedWall;
        public UnityEvent WallDown;
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
        }
        private void Start()
        {
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
                switch (hit.type)
                {
                    case Hit.Type.Indicator:
                        break;
                    case Hit.Type.Damage:
                        SpawnedHit.Invoke();
                        foreach (var pos in hit.Locations)
                        {
                            spellElsPool.Spawn(this, hit, pos + epicenter);
                        }
                        break;
                    case Hit.Type.Wall:
                        SpawnedWall.Invoke();

                        StartCoroutine(CoroutineUtils.WaitThenDo(hit.wallDuration, OnWallDown));

                        foreach (var pos in hit.Locations)
                        {
                            spellWallElsPool.Spawn(this, hit, pos + epicenter);
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                
                }
                nextHits.Remove(hit);
            }
            if(nextHits.Count == 0)
            {
                OnFinshed();
            }
        }

        private void OnWallDown()
        {
            WallDown.Invoke();
        }

        private void OnFinshed()
        {
            StartCoroutine(CoroutineUtils.WaitThenDo(10, () => Destroy(gameObject)));
        }
        
        public class Factory : PlaceholderFactory<Card, Vector2Int, Spell>
        {

        }
    }
}