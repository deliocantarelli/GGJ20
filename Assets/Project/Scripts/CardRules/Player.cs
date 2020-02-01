using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace GGJ20.CardRules
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

        [InjectOptional]
        [SerializeField]
        private Settings settings;
        public float ManaReal { get; private set; }
        public int ManaUsable { get { return (int)Math.Floor(ManaReal); } }
        public float ManaPercent { get { return ManaReal / MaxMana; } }
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
        private void Init()
        {
            ManaReal = settings.StartingMana;
        }
        public bool TryPlayCard(Card card)
        {
            throw new NotImplementedException();
        }
        private void Update()
        {
            ManaReal += Time.deltaTime * ManaIncome;
            ManaReal = Mathf.Clamp(ManaReal, 0, MaxMana);
        }
    }
}
