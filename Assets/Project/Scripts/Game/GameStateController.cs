using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace GGJ20.Game
{
    public class GameStateController : MonoBehaviour
    {
        [Serializable]
        public class Configs
        {
            public string[] Floors;
            public string MainMenu;
        }
        [Inject]
        private Configs configs;
        [Inject]
        Run.Factory runFactory;
        [Inject]
        private ZenjectSceneLoader sceneLoader;
        public Run CurrentRun { get; private set; }

        [Inject]
        private void Init()
        {
            StartRun();
        }
        public void StartRun()
        {
            CurrentRun = runFactory.Create();
        }

        public void GoToFloorScene()
        {
            sceneLoader.LoadScene(configs.Floors[CurrentRun.Floor]);
        }
        internal void AdvanceAndLoad()
        {
            CurrentRun.Floor++;
            GoToFloorScene();
        }
        public void GoToMenu()
        {
            sceneLoader.LoadScene(configs.MainMenu);
        }

    }
}
