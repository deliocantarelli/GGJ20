
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
        public override void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.tag == "end_game_line") {
                Enemy.OnGameWon();         
            }
        }
    }
}
