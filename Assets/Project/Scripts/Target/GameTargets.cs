
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace GGJ20.Target {
    public class GameTargets
    {
        List<Targetable> targets = new List<Targetable>();

        public Targetable[] Targets { get; private set; }
        public void RegisterBattery(Targetable target)
        {
            targets.Add(target);
            Targets = targets.ToArray();
        }
        public void RemoveBattery(Targetable target) {
            targets.Remove(target);
            Targets = targets.ToArray();

            if(targets.Count == 0) {
                GameResult.Result = true;
                SceneManager.LoadScene("Endgame");
            }
        }
    }
}