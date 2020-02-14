using System;
using Common.StateMachines;

namespace GGJ20.Enemy
{
    public abstract class EnemyStateBase : State<EnemyController, EnemyStateBase>
    {
        protected EnemyController Enemy { get => Context; }

        public virtual void Update() {
            
        }

        public virtual void OnCollisionEnter2D(UnityEngine.Collision2D other) {
            
        }
        public virtual void OnCollisionExit2D(UnityEngine.Collision2D other) {

        }


        public virtual void OnDestroy()
        {
            ExitTo(new DestroyedState());
        }
        public virtual void OnGameOver()
        {
            ExitTo(new EnemyGameOverState());
        }
    }
}