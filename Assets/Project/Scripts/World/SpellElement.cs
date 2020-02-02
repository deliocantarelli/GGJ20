using System;
using DG.Tweening;
using GGJ20.Battery;
using GGJ20.CardRules;
using GGJ20.Enemy;
using GGJ20.Utils;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace GGJ20.World
{
    public abstract class SpellElement : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer sprite;
        [SerializeField]
        private Collider2D col;

        [Inject]
        private WorldGrid grid;

        public UnityEvent OnSetup;
        public UnityEvent BeforeHide;


        private Stopwatch timer = new Stopwatch();
        private Spell.Hit hit;
        private Spell spell;

        public Vector2Int GridPos { get; private set; }
        public float WallDuration { get => hit == null ? float.PositiveInfinity : hit.wallDuration; }

        private WorldGrid.Pattern pat;
        [SerializeField]
        private bool autoTogglesCollider;
        [SerializeField]
        private float toggleTime = .2f;
        [SerializeField]
        private bool usePlaceholderAnimation = true;
        [SerializeField]
        private float duration =  .5f;

        protected void Reinitialize(Spell spell, Spell.Hit hit, Vector2Int pos)
        {
            this.hit = hit;
            this.spell = spell;


            GridPos = pos;
            pat = grid.GridPattern(pos);
            transform.position = grid.GridToWorld(pos, WorldGrid.PlaceMode.TileCenter);
            Setup(spell);
        }

        internal void OnLower()
        {
            col.enabled = false;
        }

        protected virtual void Setup(Spell spell)
        {

            if (usePlaceholderAnimation)
            {
                SetAlpha(0);

                sprite.DOFade(1, duration / 2).SetLoops(2, LoopType.Yoyo)
                    .OnComplete(OnAnimationOver);
            }

            col.enabled = true;
            if(autoTogglesCollider)
                timer.Restart();

            OnSetup.Invoke();
            AfterSetup();
        }
        protected virtual void AfterSetup()
        {

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
            Hide();
        }

        public void Hide()
        {
            BeforeHide.Invoke();
            OnHide();
        }
        protected abstract void OnHide();

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

    }
}