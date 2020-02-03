using GGJ20.CardRules;
using GGJ20.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGJ20.UI
{
    public class BoosterCardPicker : CardPickerPanel
    {

        protected override void CardPicked(CardDisplay cardDisplay)
        {
            scene.OnCardToAddPicked(cardDisplay.Card);
        }

        protected override IEnumerable<Card> GenerateCards()
        {
            return game.CurrentRun.GenerateBooster(displays.Count);
        }
        protected override void OnInit()
        {
            gameObject.SetActive(false);
            this.scene.BattleResultShown += OnBattleOver;
        }
        private void OnBattleOver(GameResult result)
        {
            if (result.Won)
            {
                gameObject.SetActive(true);
                Show();
            }
        }
    }
}
