

using UnityEngine;
using System;
using Zenject;
using System.Collections.Generic;
using System.Linq;

namespace PointNSheep.Mend.Battle
{
    public class TargetsManager : MonoBehaviour {
        [Inject]
        private GameTargets gameTargets;

        public Transform GetClosestTarget(Transform otherTransform)
        {
            Targetable target = null;

            var targets = gameTargets.Targets.Where(t => !t.IsInvulnerable).ToArray();


            Vector2 pos = otherTransform.position;
            float distance = float.PositiveInfinity;
            foreach (Targetable currentTarget in targets) {
                float currentDis = Vector2.Distance(currentTarget.transform.position, pos);
                if(currentDis < distance) {
                    distance = currentDis;
                    target = currentTarget;
                }
            }
            if (target == null) {
                return null;
            }
            return target.transform;
        }
    }
}