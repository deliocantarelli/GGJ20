using GGJ20.CardRules;
using GGJ20.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGJ20.UI
{
    public class RemoveCardPicker : CardPickerPanel
    {

        protected override void CardPicked(CardDisplay cardDisplay)
        {
            scene.OnCardToRemovePicked(cardDisplay.Card);
        }

        protected override IEnumerable<Card> GenerateCards()
        {
            return game.CurrentRun.CardsInDeck;
        }

        protected override void OnInit()
        {
            this.scene.CardPicked += CardToRemovePicked;
        }

        private void CardToRemovePicked(Card obj)
        {
            Show();
        }
    }
}
