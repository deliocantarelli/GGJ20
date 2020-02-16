using UnityEngine;
using Zenject;
using DG.Tweening;
using System.Threading.Tasks;
using PointNSheep.Mend.Battle;

namespace PointNSheep.Mend.Battle
{
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