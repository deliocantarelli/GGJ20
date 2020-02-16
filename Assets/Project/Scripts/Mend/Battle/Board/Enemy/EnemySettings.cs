

using UnityEngine;

namespace PointNSheep.Mend.Battle {
    [CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemySettings", order = 1)]
    public class EnemySettings : ScriptableObject {
        public GameObject prefab;
        public float attackTimeDelay = 1;
        public int damage = 1;
        public float speed = 1;
        public int life = 1;
    }

}