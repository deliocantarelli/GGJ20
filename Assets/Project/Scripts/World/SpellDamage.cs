using AntonioHR.Interactables;
using GGJ20.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGJ20.World
{
    public class SpellDamage : ObjectTrigger<EnemyController>
    {
        private SpellElement el;
        protected override void OnTriggered(EnemyController enemy)
        {
            el.TryHit(enemy);
        }
    }
}
