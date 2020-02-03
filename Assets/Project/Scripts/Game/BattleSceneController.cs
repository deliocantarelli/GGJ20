using GGJ20.Audio;
using GGJ20.CardRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace GGJ20.Game
{
    public class BattleSceneController : MonoBehaviour
    {
        private Card pickedCard;
        [Inject]
        private GameStateController game;
        [Inject]
        private AudioManager audio;

        public GameResult Result { get; private set; }


        public event Action<GameResult> BattleOver;
        public event Action<GameResult> BattleResultShown;
        public event Action<Card> CardPicked;
        private void Start()
        {
            audio.OnGame();
        }
        public void OnWin()
        {
            Result = GameResult.GetNewVictory();
            BattleOver?.Invoke(Result);
            BattleResultShown?.Invoke(Result);
        }
        public void OnLose()
        {
            Result = GameResult.GetNewLoss();
            BattleOver?.Invoke(Result);
        }

        public void OnCardToAddPicked(Card card)
        {
            this.pickedCard = card;
            CardPicked?.Invoke(card);
        }

        public void OnCardToRemovePicked(Card card)
        {
            game.CurrentRun.Replace(card, pickedCard);
            game.AdvanceAndLoad();
        }
    }
}
