using GGJ20.CardRules;
using GGJ20.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace GGJ20.Installers
{
    [RequireComponent(typeof(ICardDisplayListener))]
    public class GenericCardPanelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ICardDisplayListener>()
                .FromInstance(GetComponent<ICardDisplayListener>())
                .AsSingle();
        }
    }
}
