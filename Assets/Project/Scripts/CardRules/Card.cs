using UnityEngine;

namespace GGJ20.CardRules
{
    [CreateAssetMenu(menuName = "GGJ20/Card")]
    public class Card : ScriptableObject
    {
        public Sprite Art;
        public int ManaCost;
        
    }
}