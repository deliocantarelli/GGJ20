using GGJ20.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GG20.Common
{
    [RequireComponent(typeof(Text))]
    public class HealthText : MonoBehaviour
    {
        private Text text;
        private IHealth health;

        [Inject]
        private void Init(IHealth health)
        {
            text = GetComponent<Text>();
            this.health = health;
            health.HealthChanged += OnHealthChanged;
            UpdateValues();
        }

        private void OnHealthChanged(IHealth health, int change)
        {
            UpdateValues();
        }

        private void UpdateValues()
        {
            text.text = string.Format("{0}/{1}", health.CurrentHealth, health.MaxHealth);
        }
    }
}
