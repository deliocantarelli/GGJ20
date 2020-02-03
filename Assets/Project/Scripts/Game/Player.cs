using GGJ20.CardRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace GGJ20.Game
{
    public class Player : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            public float ManaIncome = 1;
            public float StartingMana = 3;
            public int MaxMana = 10;
        }

        public event Action<Player> UsableManaChanged;

        [InjectOptional]
        [SerializeField]
        private Settings settings;
        public float RealMana { get; private set; }
        public int UsableMana { get { return (int)Math.Floor(RealMana); } }
        public float ManaPercent { get { return RealMana / MaxMana; } }
        private int MaxMana
        {
            get
            {
                return settings.MaxMana;
            }
        }
        private float ManaIncome
        {
            get
            { 
                return settings.ManaIncome;
            }
        }

        [Inject]
        private BattleSceneController battle;


        private void OnBattleOver(GameResult obj)
        {
            gameObject.SetActive(false);
            battle.BattleOver += OnBattleOver;
        }


        private void OnDestroy()
        {
            if (battle != null)
            {
                battle.BattleOver -= OnBattleOver;
            }
        }

        [Inject]
        private void Init()
        {
            RealMana = settings.StartingMana;
        }

        public bool CanPlayCard(Card card)
        {
            return card.ManaCost <= UsableMana;
        }
        public bool TryPlayCard(Card card)
        {
            if (!CanPlayCard(card))
                return false;


            RealMana -= card.ManaCost;
            UsableManaChanged?.Invoke(this);
            return true;
        }
        private void Update()
        {
            RunManaIncomeUpdate();
        }

        private void RunManaIncomeUpdate()
        {
            int prev = UsableMana;
            RealMana += Time.deltaTime * ManaIncome;
            RealMana = Mathf.Clamp(RealMana, 0, MaxMana);
            if (prev != UsableMana)
            {
                UsableManaChanged?.Invoke(this);
            }
        }
    }
}
