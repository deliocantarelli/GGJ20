using UnityEngine;
using Zenject;

namespace PointNSheep.Mend.Battle
{
    public class SpellAimInstaller : Zenject.MonoInstaller<SpellAimInstaller>
    {
        public override void InstallBindings()
        {
            //Container.BindFactory<UnityEngine.Object, SpellAimCursor, SpellAimCursor.Factory>().FromFactory<PrefabFactory<SpellAimCursor>>();
        }
    }
}