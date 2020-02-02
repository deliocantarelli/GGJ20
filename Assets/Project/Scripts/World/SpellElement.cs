using System;
using DG.Tweening;
using GGJ20.CardRules;
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

        [Inject]
        private WorldGrid grid;
        [Inject]
        Pool pool;

        private void Reinitialize(Card card, Vector2Int pos)
        {
            transform.position = grid.GridToWorld(pos, WorldGrid.PlaceMode.TileCenter);
            sprite.sprite = card.HitElSprite != null ? card.HitElSprite : defaultSprite;

            SetAlpha(0);

            sprite.DOFade(1, .5f).SetLoops(2, LoopType.Yoyo)
                .OnComplete(OnAnimationOver);
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
        
        public class Pool : MonoMemoryPool<Card, Vector2Int, SpellElement>
        {
            protected override void Reinitialize(Card card, Vector2Int vec, SpellElement item)
            {
                base.Reinitialize(card, vec, item);
                item.Reinitialize(card, vec);
            }
        }
    }
}