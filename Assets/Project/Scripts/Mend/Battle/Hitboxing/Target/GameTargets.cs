
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Zenject;

namespace PointNSheep.Mend.Battle
{
    public class GameTargets
    {
        List<Targetable> targets = new List<Targetable>();

        [Inject]
        private BattleSceneController sceneController;

        public IEnumerable<Targetable> Targets { get => targets.AsReadOnly(); }
        public void RegisterBattery(Targetable target)
        {
            targets.Add(target);
        }
        public void OnBatterySaved(Targetable target) {
            targets.Remove(target);

            if(targets.Count == 0) {
                sceneController.OnWin();
            }
        }

        public void OnBatteryDestroyed(Targetable target)
        {
            targets.Remove(target);
        }
    }
}