using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace GGJ20.Game
{
    public class DisableAfterBattle : MonoBehaviour
    {

        [Inject]
        private BattleSceneController battle;


        private void OnDestroy()
        {
            if(battle!=null)
            {
                battle.BattleOver -= OnBattleOver;
            }
        }
        private void Start()
        {
            battle.BattleOver += OnBattleOver;
        }

        private void OnBattleOver(GameResult obj)
        {
            gameObject.SetActive(false);
        }
    }
}
