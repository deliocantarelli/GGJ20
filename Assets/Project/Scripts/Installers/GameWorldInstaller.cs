using GGJ20.CardRules;
using GGJ20.World;
using UnityEngine;
using Zenject;

public class GameWorldInstaller : MonoInstaller
{
    [SerializeField]
    private Spell spellPrefab;
    [SerializeField]
    private SpellElement spellHitPrefab;

    public override void InstallBindings()
    {
        Container.Bind<WorldGrid>().FromComponentInHierarchy().AsSingle();

        Container.BindFactory<Card, Vector2Int, Spell, Spell.Factory>()
            .FromComponentInNewPrefab(spellPrefab)
            .UnderTransformGroup("World/Spells");
        Container.BindMemoryPool<SpellElement, SpellElement.Pool>()
            .FromComponentInNewPrefab(spellHitPrefab)
            .UnderTransformGroup("World/SpellElements");
    }
}