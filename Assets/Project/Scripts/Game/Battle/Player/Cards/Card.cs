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

        [SerializeField]
        private string description;

        private void OnValidate()
        {
            description = GetPowerText();
        }
        public string GetPowerText()
        {
            var wallHits = Spell.hits.Where(h => h.type == World.Spell.Hit.Type.Wall);
            var dmgHits = Spell.hits.Where(h => h.type == World.Spell.Hit.Type.Damage);

            string result = "";
            if (dmgHits.Any())
            {
                int count = dmgHits.Sum(h => 1 + h.repeats);
                int dmg = dmgHits.Min(h => h.damage);

                result+= string.Format("{0}x{1}", count, ColoredText(dmg.ToString(), Color.red));
            }

            if(wallHits.Any())
            {
                if (result.Length > 0)
                    result += "-";
                result += string.Format("{0}s", ColoredText(wallHits.Average(h => h.wallEnd).ToString(), Color.green));
            }

            return result;
        }

        private object ColoredText(string txt, Color color)
        {
            if(Application.isPlaying)
            {
                return string.Format("<color={0}>{1}</color>", ColorUtility.ToHtmlStringRGB(color), txt);
            } else

            {
                return txt;
            }
        }
    }
}