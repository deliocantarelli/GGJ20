using UnityEngine;

namespace PointNSheep.Mend.Battle {
    public class EnemyGameOverState : EnemyStateBase
    {
        public EnemyGameOverState() {
        }
        protected override void Begin()
        {
            Enemy.SetMovement(false);
            Enemy.endGameObject.SetActive(true);
        }

        public override void OnGameOver()
        {
        }
        public override void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.tag == "end_game_line") {
                Enemy.OnGameWon();
            }
        }
    }
}
