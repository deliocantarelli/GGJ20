

using UnityEngine;
using System;
using Zenject;
using System.Collections.Generic;

namespace GGJ20.Target
{
    public class TargetsManager : MonoBehaviour {
        [Inject]
        private GameTargets gameTargets;

        public Transform GetClosestTarget(Transform otherTransform)
        {
            Vector2 pos = otherTransform.position;

            float distance = float.PositiveInfinity;
            Targetable target = null;

            Targetable[] targets = gameTargets.Targets;
            if(targets == null) {
                return null;
            }
            foreach (Targetable currentTarget in targets) {
                float currentDis = Vector2.Distance(currentTarget.transform.position, pos);
                if(currentDis < distance) {
                    distance = currentDis;
                    target = currentTarget;
                }
            }
            return target.transform;
        }
    }
}