using UnityEngine;
using Zenject;
using GGJ20.Game;
using DG.Tweening;

namespace GGJ20.Grid {
    public class ChangeTile : MonoBehaviour {
        public float timeToFinish;
        [Inject]
        private BattleSceneController battle;
        void Start()
        {
            battle.BattleOver += OnBattleOver;
            ChangeTilesToGreen();
        }
        private void OnBattleOver(GameResult obj) {
            if(obj.Won) {
                ChangeTilesToGreen();
            }
        }

        private void ChangeTilesToGreen() {
            transform.DOLocalMoveY(22, timeToFinish);
        }
    }
}