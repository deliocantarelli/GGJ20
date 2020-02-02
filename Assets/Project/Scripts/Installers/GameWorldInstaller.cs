using GGJ20.CardRules;
using GGJ20.World;
using UnityEngine;
using Zenject;

public class GameWorldInstaller : MonoInstaller
{
    [SerializeField]
    private Spell spellPrefab;
    [SerializeField]
    private HitSpellElement spellHitPrefab;
    [SerializeField]
    private WallSpellElement spellWallElementPrefab;

    public override void InstallBindings()
    {
        Container.Bind<WorldGrid>().FromComponentInHierarchy().AsSingle();

        Container.BindFactory<Card, Vector2Int, Spell, Spell.Factory>()
            .FromComponentInNewPrefab(spellPrefab)
            .UnderTransformGroup("World/Spells");
        Container.BindMemoryPool<HitSpellElement, HitSpellElement.Pool>()
            .WithInitialSize(10)
            .FromComponentInNewPrefab(spellHitPrefab)
            .UnderTransformGroup("World/SpellElements/Hit");

        Container.BindMemoryPool<WallSpellElement, WallSpellElement.Pool>()
            .WithInitialSize(10)
            .FromComponentInNewPrefab(spellWallElementPrefab)
            .UnderTransformGroup("World/SpellElements/Wall");
    }
}