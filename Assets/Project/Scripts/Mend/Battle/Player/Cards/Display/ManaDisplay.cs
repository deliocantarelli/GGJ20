﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PointNSheep.Mend.Battle
{
    public class ManaDisplay : MonoBehaviour
    {
        [SerializeField]
        private Image slider;
        [SerializeField]
        private Text text;

        [Inject]
        private Player player;
        

        [Inject]
        private void Init()
        {
            player.UsableManaChanged += UpdateIndicator;

            UpdateIndicator(player);
        }
        private void OnDestroy()
        {
            player.UsableManaChanged -= UpdateIndicator;
        }
        private void LateUpdate()
        {
            slider.fillAmount = player.ManaPercent;
        }

        private void UpdateIndicator(Player player)
        {
            text.text = player.UsableMana.ToString();
        }
    }
}
