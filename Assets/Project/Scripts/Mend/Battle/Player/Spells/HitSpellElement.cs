using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
namespace PointNSheep.Mend.Battle
{
    public class HitSpellElement : SpellElement
    {

        [Inject]
        Pool pool;
        protected override void OnHide()
        {
            pool.Despawn(this);
        }

        public class Pool : MonoMemoryPool<Spell, Spell.Hit, Vector2Int, HitSpellElement>
        {
            protected override void Reinitialize(Spell spell, Spell.Hit hit, Vector2Int vec, HitSpellElement item)
            {
                base.Reinitialize(spell, hit, vec, item);
                item.Reinitialize(spell, hit, vec);
            }
        }
    }
}
