using GGJ20.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GGJ20.World.Spell;

namespace PointNSheep.Board
{
    public class SpellHitChecker
    {
        private float hitReset = .2f;

        private Stopwatch timer = new Stopwatch();
        private List<Spell.Hit> currentHits = new List<Spell.Hit>();

        public bool CheckHit(Hit hit, out int damage)
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
