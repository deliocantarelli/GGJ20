using GGJ20.CardRules;
using UnityEngine;
using Zenject;

namespace GGJ20.Installers
{
    public class SpellAimInstaller : Zenject.MonoInstaller<SpellAimInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<UnityEngine.Object, SpellAimCursor, SpellAimCursor.Factory>().FromFactory<PrefabFactory<SpellAimCursor>>();
        }
    }
}