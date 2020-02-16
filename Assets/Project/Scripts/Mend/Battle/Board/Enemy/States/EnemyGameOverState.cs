using UnityEngine;

namespace PointNSheep.Mend.Battle {
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
