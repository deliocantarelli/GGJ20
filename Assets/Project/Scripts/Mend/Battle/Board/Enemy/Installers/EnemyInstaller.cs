using UnityEngine;
using Zenject;
namespace PointNSheep.Mend.Battle
{
    public class EnemyInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.BindFactory<UnityEngine.Object, EnemyController, EnemyController.Factory>().FromFactory<PrefabFactory<EnemyController>>();
        }
    }
}