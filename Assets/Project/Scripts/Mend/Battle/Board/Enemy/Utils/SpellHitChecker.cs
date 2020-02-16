using PointNSheep.Common.Timers;
using PointNSheep.Mend.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointNSheep.Mend.Battle
{
    public class SpellHitChecker
    {
        private float hitReset = .2f;

        private Stopwatch timer = new Stopwatch();
        private List<Spell.Hit> currentHits = new List<Spell.Hit>();

        public bool CheckHit(Spell.Hit hit, out int damage)
        {
            if (!currentHits.Contains(hit))
            {
                currentHits.Add(hit);
                damage = hit.damage;
                timer.Restart();
                return true;
            } else
            {
                damage = hit.damage;
                return false;
            }
        }

        public void CheckReset()
        {
            if(timer.ElapsedSeconds > hitReset)
            {
                currentHits.Clear();
                timer.ClearAndStop();
            }
        }
    }
}
