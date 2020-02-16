using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace PointNSheep.Mend.Battle
{
    public class ManualDeckInstaller : MonoInstaller
    {
        [SerializeField]
        private List<Card> cards;


        public override void InstallBindings()
        {
            Container.BindInstances(cards);
        }
    }
}
