using AntonioHR.Interactables;
using GGJ20.Battery;
using GGJ20.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGJ20.World
{
    public class SpellHeal : ObjectTrigger<BatteryController>
    {
        private SpellElement el;
        protected override void OnTriggered(BatteryController battery)
        {
            el.TryHeal(battery);
        }
    }
}
