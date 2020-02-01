using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GGJ20.CardRules
{
    public class CardDisplay : MonoBehaviour
    {
        private Card card;

        [SerializeField]
        private Image art;
        [SerializeField]
        private Text manaCost;

        [Inject]
        private void Init(Card card)
        {
            this.card = card;
            this.art.sprite = card.Art;
            this.manaCost.text = card.ManaCost.ToString();
        }
    }
}
