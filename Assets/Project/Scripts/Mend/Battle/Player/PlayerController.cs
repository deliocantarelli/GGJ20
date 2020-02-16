using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace PointNSheep.Mend.Battle
{
    public class PlayerController : MonoBehaviour, ICardDisplayListener
    {
        private Player player;
        private Deck deck;
        private SpellAimController spellAim;



        [Inject]
        private void Init(Player player, Deck deck, SpellAimController spellAim)
        {
            this.player = player;
            this.deck = deck;
            this.spellAim = spellAim;

        }


        public void OnSelected(CardDisplay cardDisplay)
        {
            cardDisplay.ShowHeld();
            spellAim.StartAiming(cardDisplay);
        }
        public bool OnConfirmed(CardDisplay cardDisplay)
        {
            if (spellAim.IsGridPosValid)
            {
                if (player.TryPlayCard(cardDisplay.Card))
                {
                    OnCardUsed(cardDisplay);
                    spellAim.SpawnSpell();
                    return true;
                }
            } else
            {
                cardDisplay.Deselect();
                spellAim.StopAiming();
            }
            return false;
        }
        public void OnCardUsed(CardDisplay cardDisplay)
        {
            spellAim.StopAiming();
            StartCoroutine(CardUseAndRestock(cardDisplay));
        }

        private IEnumerator CardUseAndRestock(CardDisplay cardDisplay)
        {
            yield return cardDisplay.UsageAnimationCoroutine();

            deck.PutCardOnBottom(cardDisplay.Card);
            cardDisplay.SetCard(deck.Draw());

            yield return cardDisplay.NewCardCoroutine();
        }

    }
}
