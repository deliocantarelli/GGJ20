
using System;
using PointNSheep.Common.StateMachines;
using UnityEngine;

namespace PointNSheep.Mend.Battle
{
    public class EnemyStateMachine : StateMachine<EnemyController, EnemyStateBase>
    {
        public override EnemyStateBase DefaultState => new EnemyPursueState();

        public void Update() {
            CurrentState.Update();
        }
        public void OnDestroy()
        {
            CurrentState.OnDestroy();
        }
        public void OnCollisionEnter2D(Collision2D other)
        {
            CurrentState.OnCollisionEnter2D(other);
        }

        public void OnGameOver()
        {
            CurrentState.OnGameOver();
        }
        public void OnCollisionExit2D(Collision2D other) {
            CurrentState.OnCollisionExit2D(other);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            CurrentState.OnTriggerEnter2D(other);
        }
    }
}