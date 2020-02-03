using DG.Tweening;
using GGJ20.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Project.Scripts.UI
{
    public class LossPanel : MonoBehaviour
    {
        private BattleSceneController scene;
        private GameStateController game;

        [SerializeField]
        private Text text;
        [SerializeField]
        private Button button;
        [SerializeField]
        private string msg = "You've reached Floor {0}";

        [Inject]
        private void Inject(BattleSceneController scene, GameStateController game)
        {
            this.scene = scene;
            this.game = game;
            scene.BattleOver += OnBattleOver;

            gameObject.SetActive(false);

            text.text = string.Format(msg, game.CurrentRun.Floor);
            button.onClick.AddListener(Advance);
        }

        private void Advance()
        {
            game.GoToMenu();
        }

        private void OnBattleOver(GameResult obj)
        {
            if(!obj.Won)
            {
                gameObject.SetActive(true);
            }


            transform.DOMoveY(-400, .5f).From().SetRelative();
        }
    }
}
