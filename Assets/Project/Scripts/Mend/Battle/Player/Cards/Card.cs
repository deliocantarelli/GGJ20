using System;
using System.Linq;
using UnityEngine;

namespace PointNSheep.Mend.Battle
{
    [CreateAssetMenu(menuName = "MEND/Card")]
    public class Card : ScriptableObject
    {
        public Sprite Art;
        public Sprite ShapeArt;
        public int ManaCost;
        public Spell.Description Spell;
        public Sprite Background;
        [SerializeField]
        private bool customDescription = false;

        [SerializeField]
        private string description;

        private void OnValidate()
        {
            if(!customDescription) {
                description = GetPowerText();
            }
        }
        public string GetPowerText()
        {
            if(customDescription) {
                return description;
            }
            var wallHits = Spell.hits.Where(h => h.type == Battle.Spell.Hit.Type.Wall);
            var dmgHits = Spell.hits.Where(h => h.type == Battle.Spell.Hit.Type.Damage);

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