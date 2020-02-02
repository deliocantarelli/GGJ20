using System;
using DG.Tweening;
using GGJ20.Battery;
using GGJ20.CardRules;
using GGJ20.Enemy;
using GGJ20.Utils;
using UnityEngine;
using Zenject;

namespace GGJ20.World
{
    public class SpellElement : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer sprite;
        [SerializeField]
        private Sprite defaultSprite;
        [SerializeField]
        private Collider2D col;

        [Inject]
        private WorldGrid grid;
        [Inject]
        Pool pool;


        private Stopwatch timer = new Stopwatch();
        private Spell.Hit hit;
        private Spell spell;
        private float toggleTime = .2f;

        private void Reinitialize(Spell spell, Spell.Hit hit, Vector2Int pos)
        {
            this.hit = hit;
            this.spell = spell;

            transform.position = grid.GridToWorld(pos, WorldGrid.PlaceMode.TileCenter);
            sprite.sprite = spell.Card.HitElSprite != null ? spell.Card.HitElSprite : defaultSprite;

            SetAlpha(0);

            sprite.DOFade(1, .5f).SetLoops(2, LoopType.Yoyo)
                .OnComplete(OnAnimationOver);

            col.enabled = true;
            timer.Restart();
        }
        private void Update()
        {
            if(timer.ElapsedSeconds > toggleTime)
            {
                timer.ClearAndStop();
                col.enabled = false;
            }
        }

        private void OnAnimationOver()
        {
            pool.Despawn(this);
        }

        private void SetAlpha(float alpha)
        {
            var c = sprite.color;
            c.a = alpha;
            sprite.color = c;
        }


        public void TryHeal(BatteryController battery)
        {
            battery.TryHeal(hit);
        }

        public void TryHit(EnemyController enemy)
        {
            enemy.TryHit(hit);
        }

        public class Pool : MonoMemoryPool<Spell, Spell.Hit,  Vector2Int, SpellElement>
        {
            protected override void Reinitialize(Spell spell, Spell.Hit hit, Vector2Int vec, SpellElement item)
            {
                base.Reinitialize(spell, hit, vec, item);
                item.Reinitialize(spell, hit, vec);
            }
        }
    }
}