using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
// using UnityEngine.UI;
using Zenject;

namespace GGJ20.Cards
{
    public class ManaDisplay : MonoBehaviour
    {
        [SerializeField]
        // private Slider slider;

        [Inject]
        private Player player;

        private void LateUpdate()
        {
            // slider.value = player.ManaPercent;
        }
    }
}
