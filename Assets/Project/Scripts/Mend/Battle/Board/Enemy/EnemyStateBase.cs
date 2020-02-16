using PointNSheep.Common.StateMachines;
using System;
using Common.StateMachines;
using UnityEngine;

namespace PointNSheep.Mend.Battle
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

        public virtual void OnTriggerEnter2D(Collider2D other)
        {

        }
    }
}