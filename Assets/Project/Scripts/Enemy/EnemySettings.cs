

using UnityEngine;

namespace GGJ20.Enemy {
    [CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemySettings", order = 1)]
    public class EnemySettings : ScriptableObject {
        public float attackTimeDelay = 1;
        public float damage = 1;
        public float speed = 1;
    }

}