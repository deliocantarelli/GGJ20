using PointNSheep.Common.Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;


namespace PointNSheep.Mend.Battle
{
    public class WallSpellElement : SpellElement
    {
        [Inject]
        Pool pool;
        [SerializeField]
        private ObstacleAppear obstacle;
        protected override void OnHide()
        {
            pool.Despawn(this);

            obstacle.enabled = false;
        }

        protected override void AfterSetup()
        {
            StartCoroutine(UpdateCol());
        }

        private IEnumerator UpdateCol()
        {
            yield return new WaitForSeconds(.5f);
            obstacle.enabled = true;
        }

        public class Pool : MonoMemoryPool<Spell, Spell.Hit, Vector2Int, WallSpellElement>
        {
            protected override void Reinitialize(Spell spell, Spell.Hit hit, Vector2Int vec, WallSpellElement item)
            {
                base.Reinitialize(spell, hit, vec, item);
                item.Reinitialize(spell, hit, vec);
            }
        }
    }
}
