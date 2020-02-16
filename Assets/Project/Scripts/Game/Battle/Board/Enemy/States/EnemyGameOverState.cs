
using GGJ20.Target;
using UnityEngine;

namespace GGJ20.Enemy {
    public class EnemyGameOverState : EnemyStateBase
    {
        public EnemyGameOverState() {
        }
        protected override void Begin()
        {
            Enemy.SetMovement(false);
        }

        public override void OnGameOver()
        {
        }
    }
}
