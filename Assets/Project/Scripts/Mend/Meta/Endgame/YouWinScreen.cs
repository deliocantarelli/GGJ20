using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PointNSheep.Mend.Meta
{
    public class YouWinScreen : MonoBehaviour
    {
       
        public Text text;
        public Button bttn;
        [TextArea]
        public string msg = "Thank You For Playing!/nYour Time: {0}";
        private GameStateController game;

        [Inject]
        public void Inject(GameStateController game)
        {
            this.game = game;
            text.text = string.Format(msg, game.CurrentRun.DurationString);
            bttn.onClick.AddListener(StartNewRun);
        }

        private void StartNewRun()
        {
            game.StartRun();
            game.GoToFloorScene();
        }
    }
}
