using PointNSheep.Common.Interactables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PointNSheep.Mend.Battle
{
    public class SpellHeal : ObjectTrigger<BatteryController>
    {
        [SerializeField]
        private SpellElement el;
        protected override void OnTriggered(BatteryController battery)
        {
            el.TryHeal(battery);
        }
    }
}
