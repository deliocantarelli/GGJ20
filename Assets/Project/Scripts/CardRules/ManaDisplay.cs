using GGJ20.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GGJ20.CardRules
{
    public class ManaDisplay : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private Text text;

        [Inject]
        private Player player;
        

        [Inject]
        private void Init()
        {
            player.UsableManaChanged += UpdateIndicator;
        }
        private void OnDestroy()
        {
            player.UsableManaChanged -= UpdateIndicator;
        }
        private void LateUpdate()
        {
            slider.value = player.ManaPercent;
        }

        private void UpdateIndicator(Player player)
        {
            text.text = player.UsableMana.ToString();
        }
    }
}
