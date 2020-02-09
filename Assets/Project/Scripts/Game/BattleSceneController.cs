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

        public delegate Task RunAnimation();
        private List<RunAnimation> onWinAnimations = new List<RunAnimation>();
        private List<RunAnimation> onLossAnimations = new List<RunAnimation>();


        public event Action<GameResult> BattleOver;
        public event Action<GameResult> BattleResultShown;
        public event Action<Card> CardPicked;
        private void Start()
        {
            audio.OnGame();
        }
        public async void OnWin()
        {
            Result = GameResult.GetNewVictory();
            BattleOver?.Invoke(Result);

            await WaitAnimations(onWinAnimations);

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

        private async Task WaitAnimations(List<RunAnimation> animations) {
            Task[] tasks = new Task[animations.Count];
            for(int i = 0; i < animations.Count; i ++) {
                RunAnimation animation = animations[i];
                tasks[i] = animation();
            }
            try {
                await Task.WhenAll(tasks);
            } catch(Exception e) {
                Debug.LogError("problem playing end game animations");
                Debug.LogError(e);
            }
        }
        public void RegisterOnWinAnimations(RunAnimation animation) {
            onWinAnimations.Add(animation);
        }
        public void RegisterOnLossAnimations(RunAnimation animation) {
            onLossAnimations.Add(animation);
        }
    }
}
