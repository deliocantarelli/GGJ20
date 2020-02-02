using AntonioHR.Interactables;
using GGJ20.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ20.World
{
    public class SpellDamage : ObjectTrigger<EnemyController>
    {
        [SerializeField]
        private SpellElement el;
        protected override void OnTriggered(EnemyController enemy)
        {
            el.TryHit(enemy);
        }
    }
}
