using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PointNSheep.Mend.Battle
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
            gameObject.SetActive(false);
            this.scene.CardPicked += CardToAddPicked;
        }

        private void CardToAddPicked(Card obj)
        {
            gameObject.SetActive(true);
            Show();
        }
    }
}
