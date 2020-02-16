using UnityEngine;
using Zenject;
using GGJ20.Game;
using DG.Tweening;
using System.Threading.Tasks;

namespace GGJ20.Grid {
    public class ChangeTile : MonoBehaviour {
        [SerializeField]
        private float delayToStart;
        [SerializeField]
        private float timeToFinish;
        [Inject]
        private BattleSceneController battle;
        void Start()
        {
            battle.RegisterOnWinAnimations(OnGameWin);
        }
        private Task OnGameWin() {
            return ChangeTilesToGreen();
        }

        private Task ChangeTilesToGreen() {
            TaskCompletionSource<object> taskSource = new TaskCompletionSource<object>();
            transform.DOLocalMoveY(22, timeToFinish)
            .SetDelay(delayToStart)
            .onComplete = () => {
                taskSource.TrySetResult(null);
            };
            return taskSource.Task;
        }
    }
}