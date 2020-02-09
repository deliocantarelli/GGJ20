using System;
using Common.StateMachines;
using UnityEngine;

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
            
        }
        public virtual void OnGameOver()
        {
            ExitTo(new EnemyGameOverState());
        }

        public virtual void OnTriggerEnter2D(Collider2D other)
        {

        }
    }
}