using System;
using System.Linq;
using GGJ20.World;
using UnityEngine;

namespace GGJ20.CardRules
{
    [CreateAssetMenu(menuName = "GGJ20/Card")]
    public class Card : ScriptableObject
    {
        public Sprite Art;
        public Sprite ShapeArt;
        public int ManaCost;
        public Spell.Description Spell;
        public Sprite Background;

        public string GetPowerText()
        {
            var dmgHits = Spell.hits.Where(h => h.type == World.Spell.Hit.Type.Damage);

            if (!dmgHits.Any())
                return "";

            int count = dmgHits.Sum(h=>1+h.repeats);
            int dmg = dmgHits.Min(h => h.damage);

            return string.Format("{0}x<color=red>{1}</color>", count, dmg);
        }
    }
}