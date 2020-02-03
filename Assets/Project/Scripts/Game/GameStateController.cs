using GGJ20.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace GGJ20.Game
{
    public class GameStateController : MonoBehaviour
    {
        [Serializable]
        public class Configs
        {
            public enum LevelMode { Regex, List}

            public LevelMode levelMode;
            public string[] Floors;
            public string regex;
            public string MainMenu;
            public string WinScene;
            public int levelCount = 10;
        }
        [Inject]
        private Configs configs;
        [Inject]
        Run.Factory runFactory;
        [Inject]
        private ZenjectSceneLoader sceneLoader;
        [Inject]
        AudioManager audioManger;
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
            string sceneName;

            switch (configs.levelMode)
            {
                case Configs.LevelMode.Regex:
                    sceneName = string.Format(configs.regex, CurrentRun.Floor + 1);
                    break;
                case Configs.LevelMode.List:
                    sceneName = configs.Floors[CurrentRun.Floor];
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (CurrentRun.Floor + 1 > configs.levelCount)
            {
                OnWin();
            } else
            {
                //Advance
                sceneLoader.LoadScene(sceneName);
                audioManger.OnGame();
            }
        }

        private void OnWin()
        {
            CurrentRun.OnWin();
            sceneLoader.LoadScene(configs.WinScene);
            audioManger.OnMenu();
        }

        internal void AdvanceAndLoad()
        {
            CurrentRun.Floor++;
            GoToFloorScene();
        }
        public void GoToMenu()
        {
            sceneLoader.LoadScene(configs.MainMenu);
            audioManger.OnMenu();
        }

    }
}
